# Unity Tilemap Loop

This demo project demonstrates one method of creating a seamless repeating
2D map in Unity.

Basically, we create three additional cameras to cover the offscreen space,
then teleport the player to the opposite end of the map when they go
out of bounds. The extra cameras get repositioned each frame to give the
illusion that the map infinitely loops back over itself like a torus.

Not yet implemented but in theory would work:

- Add TargetableExtents component to player so that enemies can target the player across map boundaries
- If dynamic rigidbody exists, set to kinematic during teleport and preserve velocity

I'm sure there will be other edge cases that will crop up depending on your use case!
In which case just need to think outside the box ;)
