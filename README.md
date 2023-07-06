# DealeronCodeTest

This solution solves the Mars Rovers problem from the DealerOn code test set.

The main approach was to recognize the movements as a series of vector changes. This points at utilizing a 2D co-ordinate system represented by matrices which also allows a general solution. Theoretically this code could be quickly expanded to include any rotation and not just the cardinal points. This could be done either by adding more supported compass points such as NE, and using a smaller theta in the precomputered L/R matrices, or by computing the rotation matrix every time instead of the set rotation matrices.

With that in mind, the code sort of organized itself. The Mars class is the general state of the system. What input it's reading, the list of rovers, and the grid mapped on to it. The grid is the max coordinates and input validation. Then finally the rover, which has it's location and direction and stores it's path for when it's time for it to move.

Some of the assumptions made:
 1) Grid Spaces were large enough for all rovers to pass each other if needed. This eliminated the need for each rover to test where the others were while pathing. In general zero concern was given to obstacles of any sort.
 2) The goal is for the rover to actually traverse the specified path and not just to compute the end destination. If the end destination was truly the only desired output, an alternative looking at path reduction may have been considered. (For example, LRLRM is the same effective path as M and LMLMLMLM is the same as doing nothing). This could be useful for large path lengths.
 3) Unit tests are not needed to cover every scenario. Some are useful to show logic and how things work but code development, not testing, is the real point of this test.
 4) Rovers will always move sequentially. No consideration to architecting for multi-threading was given.
 5) The path input is already calculted to take place in 4 hours or less and power management or other external contraints are not needed to be accounted for.
 6) Some minor quirks are allowed. In only 8 hours or less a full GUI or fleshed out interface isn't going to happen.
