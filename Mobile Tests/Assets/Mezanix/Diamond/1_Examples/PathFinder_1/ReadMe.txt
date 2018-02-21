PathFinder_1

Like the PathFinder example. This one uses the new modulo (remainder)
to clamp the incremented targets index.

Use:
Open the scene PathFinder_1, play it, And the AI will find by itself
5 targets (waypoint).

Important features used (see the ExPathFinder_1 graph):
1. NavMeshAgent: set destination.
2. NavMeshAgent: get remaining distance.
3. Transform: get position.
4. List of gameObjects (inventory case): use a gameobject by its index.
5. Increment an int variable.
6. Use int modulo to have the remainder of a division. Another way to 
	clamp an int between zero and a value.