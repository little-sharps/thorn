require 'albacore'

@settings = {
	:Config => :Debug
}

task :default => ["build"] do
end

desc "Compiles and generates CSS from Less"
task :build => ["ci:compile"] do
end

desc "Compiles and runs specs"
task :specs => ["ci:compile", "ci:specs"] do
end

namespace :ci do 
	task :full => ["ci:release", "ci:write_version", "ci:compile", "ci:specs"] do
	end
	
	desc "Set build configuration to Release"
	task :release do
		@settings[:Config] = :Release
	end
	
	desc "Write version info into assembly"
	assemblyinfo :write_version do |asm|
		patch_level = '1.0.0'
		build_number = ENV['BUILD_NUMBER'] || "0"
		version_string = "#{patch_level}.#{build_number}"
		asm.version = version_string
		asm.file_version = version_string
		asm.output_file = File.dirname(__FILE__) + "/Thorn/Properties/AssemblyVersionInfo.cs"
	end
	
	desc "Compile the VS solution with MSBuild"
	msbuild :compile do |msb|
		msb.use :net40
		msb.properties :configuration => @settings[:Config]
		msb.targets :Clean, :Build
		#msb.log_level = :verbose
		msb.verbosity = "minimal" #quiet, minimal, normal, detailed diagnostic
		msb.solution = File.dirname(__FILE__) + "/thorn.sln"
	end
	
	desc "Runs the Specs"
	exec :specs do |cmd|
		cmd.command = "packages/NUnit.#{nunit_version}/tools/nunit-console.exe"
		cmd.parameters = "Thorn.Specs/bin/#{@settings[:Config]}/Thorn.Specs.dll"
	end
end

def nunit_version
	require 'nokogiri'
	packages_file = File.new("Thorn.Specs/packages.config", "r")
	packages_xml = Nokogiri::XML(packages_file)
	packages_xml.css("package#NUnit").first['version']
end
