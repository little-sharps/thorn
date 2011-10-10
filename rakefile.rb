require 'albacore'

@settings = {
	:patch_level => '1.0.1',
	:config => :Debug
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
	task :full => ["ci:release", "ci:write_version", "ci:specs", "ci:package"] do
	end
	
	desc "Set build configuration to Release"
	task :release do
		@settings[:config] = :Release
	end
	
	desc "Write version info into assembly"
	assemblyinfo :write_version do |asm|
		build_number = ENV['BUILD_NUMBER'] || "0"
		version_string = "#{@settings[:patch_level]}.#{build_number}"
		asm.version = version_string
		asm.file_version = version_string
		asm.output_file = File.dirname(__FILE__) + "/Thorn/Properties/AssemblyVersionInfo.cs"
	end
	
	desc "Compile the VS solution with MSBuild"
	msbuild :compile do |msb|
		msb.use :net40
		msb.properties :configuration => @settings[:config]
		msb.targets :Clean, :Build
		#msb.log_level = :verbose
		msb.verbosity = "minimal" #quiet, minimal, normal, detailed diagnostic
		msb.solution = File.dirname(__FILE__) + "/thorn.sln"
	end
	
	desc "Runs the Specs"
	exec :specs => ["ci:compile"] do |cmd|
		cmd.command = "packages/NUnit.#{nunit_version}/tools/nunit-console.exe"
		cmd.parameters = "Thorn.Specs/bin/#{@settings[:config]}/Thorn.Specs.dll"
	end

	desc "Build nuget package"
	exec :package => ["ci:package_clean", "ci:package_stage"] do |cmd|
		cmd.command = "nuget"
		cmd.parameters = "pack pkg/thorn.nuspec -o pkg"
	end

	task :package_stage => ["ci:compile", "ci:ensure_package_folder", "ci:nuspec"] do
		cp "Thorn/bin/#{@settings[:config]}/Thorn.dll", "pkg/lib/Net40"
	end
	
	task :nuspec => ["ci:ensure_package_folder"] do
		File.open("pkg/thorn.nuspec", "w") do |outfile|
			IO.foreach("thorn.nuspec.template") do |line|
				line = line.gsub /::patch_level::/, @settings[:patch_level]
				line = line.gsub /::args_version::/, args_version
				outfile.puts line
			end
		end
	end

	task :ensure_package_folder do
		ensure_folder "pkg"
		ensure_folder "pkg/lib"
        ensure_folder "pkg/lib/Net40"
	end
	
	task :package_clean do
		rm_r "pkg" if Dir.exists? "pkg"
	end
end

def nunit_version
	package_version "Thorn.Specs", "NUnit"
end

def args_version
	package_version "Thorn", "Args"
end

def package_version dependant_assembly, package_name
	require 'nokogiri'
	packages_file = File.new("#{dependant_assembly}/packages.config", "r")
	packages_xml = Nokogiri::XML(packages_file)
	packages_xml.css("package##{package_name}").first['version']
end

def ensure_folder folder_name
	Dir.mkdir folder_name unless Dir.exists? folder_name
end

