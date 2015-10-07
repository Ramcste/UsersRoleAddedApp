param($installPath, $toolsPath, $package, $project)
$project.Object.References.Remove("CodeFirstStoredProcs") | out-null