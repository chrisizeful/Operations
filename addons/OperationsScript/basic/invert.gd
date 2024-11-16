class_name InvertOperation
extends Operation
## Fails when a child succeeds and succeeds when a child fails, inverting the result.

func act(delta : float) -> Status:
	children[0].run(delta)
	return Status.Running

func child_success():
	fail()

func child_fail():
	success()
