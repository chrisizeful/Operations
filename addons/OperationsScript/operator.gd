class_name Operator
extends Object
## An operator is responsible for running a list of operations. It also provides static methods
## for running individual operations and loading operations from files.

## Validators to determine if a <see cref="Operation.Target"/> is ... valid. Contains a Node validator <see cref="IsNodeValid"/> by default.
static var target_validators := {
	typeof(Node): is_node_valid
}
## All of the operations currently being processed.
var operations := []
## A SceneTree reference for determining if operations should be processed in accordance with their <see cref="Operation.ProcessMode"/>.
## The <see cref="SceneTree.Root"/> is also used as a target if one is not set on an operation.
var tree : SceneTree

func _init(tree : SceneTree):
	self.tree = tree

## Processes all added operations.
func process():
	## Iterate backwards through operations, running and then freeing them
	for i in range(operations.size() - 1, -1, -1):
		var operation = operations[i]
		if process_single(tree, operation):
			operations.remove_at(i)

func add(operation : Operation):
	if !operation.target:
		operation.set_target(tree.root)
	operations.append(operation)

## The default validator that determines if a <see cref="Node"/> is valid.
## <param name="operation">The operation whose target to check.</param>
## <returns>If the target is valid and not queued for deletion.</returns>
static func is_node_valid(operation):
	var target = operation.target
	return is_instance_valid(target) && !target.is_queued_for_deletion()

## Process a single operation.
## <param name="tree">A reference to the SceneTree.</param>
## <param name="operation">The operation to process.</param>
## <returns>If the operation has finished processing (succeeded, failed, or was cancelled).</returns>
static func process_single(tree : SceneTree, operation : Operation):
	## Check pause mode - always/inherit fall through
	if operation.process_mode == Node.PROCESS_MODE_DISABLED:
		return false
	if !tree.paused and operation.process_mode == Node.PROCESS_MODE_WHEN_PAUSED:
		return false
	if tree.paused and operation.process_mode == Node.PROCESS_MODE_PAUSABLE:
		return false
	## Remove if target is invalidated or operation succeeded, failed, or cancelled
	if !operation.target:
		return true
	if operation.current != Operation.Status.Running and operation.current != Operation.Status.Fresh:
		return true
	if !operation.target_validator || !operation.target_validator.call(operation):
		return true
	# Run
	operation.run(tree.root.get_process_delta_time())
	return false
