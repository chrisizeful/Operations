class_name Files
extends Object
## A file utility class.

## List all files of <paramref name="type"/> at the given <paramref name="path"/> (non-recursive).
## <param name="path">The path to a directory.</param>
## <param name="type">The type of files to list, or null for all files.</param>
## <returns>A list of all file paths from the directory.</returns>
static func list_files(path : String, type : String = "") -> Array[String]:
	var files := []
	var dir := DirAccess.open(path)
	dir.include_hidden = false
	dir.include_navigational = false
	_add_files(dir, type, files)
	dir.free()
	return files

static func _add_files(dir : DirAccess, type : String, files : Array[String]):
	if dir.list_dir_begin() != OK:
		return
	var next := dir.get_next()
	while next != "":
		var path := dir.get_current_dir() + "/" + next
		if dir.current_is_dir():
			if type == null || path.ends_with(type):
				files.append(path)
		next = dir.get_next()
	dir.list_dir_end()

## Randomly select a string from the list of files.
## <param name="files">A list of string paths.</param>
## <returns>A random string from files</returns>
static func random(files : Array[String]) -> String:
	return files[randi_range(0, files.size() - 1)]
