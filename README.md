Description
When you click on a cube, it disappears, and new cubes spawn in a random quantity. Each new cube is half the size of the previous one.

Core Mechanics:
- The chance of splitting decreases by half with each new generation of cubes
- If the split succeeds, the cube divides; otherwise, it explodes, pushing nearby cubes away
- The explosion affects a specific area, with its force decreasing based on distance from the center
- Smaller cubes have a larger explosion radius and greater force to scatter objects

Technical Features:
- Explosion physics dynamically calculate object repulsion forces
- Object pooling optimizes cube creation and destruction
- Recursive cube splitting with progressive chance reduction
