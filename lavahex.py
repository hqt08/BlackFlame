import pygame, sys
import random
random.seed()

BLACK = (  0,   0,   0)
WHITE = (255, 255, 255)
RED = (255,   0,   0)
GREEN = (  0, 255,   0)
BLUE = (  0,   0, 255)

pygame.init()
screen = pygame.display.set_mode((500, 500))
done = False

# hexagon tile managers
class TileManager:
        def __init__(self, height, width):
                self.height = height
                self.width = width
                self.cells = [[1 for y in xrange(0,height)] for x in xrange(0,width)]
                self.draw_stack = self.createRandStack()
                self.dontdraw_stack = []

        def createRandStack(self):
                randS = []
                size = self.width * self.height
                # add first elt
                x = random.randint(0,self.width-1)
                y = random.randint(0,self.height-1)
                randS.append(Tile(x,y,False))

                while (len(randS) < size):
                        x = random.randint(0,self.width-1)
                        y = random.randint(0,self.height-1)
                        t = Tile(x,y,False)
                        valid = True
                        for t2 in randS: # check against exisiting Tiles if already there
                                if (t2.equals(t)):
                                        valid = False
                                        break
                        if valid:
                                randS.append(t)
                return randS
                        

        def draw(self):
                for y in xrange(0,self.height):
                        for x in xrange(0,self.width):
                                if self.cells[x][y] == 1: #draw cell
                                        Tile(x,y,True)
                                else: #don't draw cell
                                        continue

        def updateTiles(self):
                # random no. of tiles to disappear
                noOfTiles = random.randint(1,4)
                # random tiles chosen
                i = 0
                while (i < noOfTiles and len(self.draw_stack)>0):
                        # x = random.randint(0,self.width-1)
                        # y = random.randint(0,self.height-1)
                        # if (self.cells[x][y] == 1):
                        #         self.cells[x][y] = 0
                        t = self.draw_stack.pop()
                        self.dontdraw_stack.append(t)
                        i += 1
                # draw tiles
                #self.draw()
                for tile in self.draw_stack:
                        #print tile.coords[0], ",", tile.coords[1]
                        tile.drawHex()
                        
l = 30
h = 26
h2 = 54
class Tile:
        def __init__(self, x, y, to_draw):
                self.coords = [x,y]
                self.center = [0,0]
                self.center[0] = x * h2 + h
                self.center[1] = y * h2 + h if x%2==0 else y * h2 + h2
                if to_draw:
                        self.drawHex()

        def drawHex(self):
                cx = self.center[0]
                cy = self.center[1]
                pygame.draw.polygon(screen, BLUE, ((cx-l, cy), (cx-h/2, cy-h), (cx+h/2, cy-h), (cx+l, cy), (cx+h/2, cy+h), (cx-h/2,cy+h)))

        def equals(self, other):
                return self.coords[0] == other.coords[0] and self.coords[1] == other.coords[1] 


# SETUP
tm = TileManager(8,8)
tm.draw()


# MAIN GAME LOOP
while not done:
        # if quitting
        for event in pygame.event.get():
                if event.type == pygame.QUIT:
                        done = True
        
        # update tiles
        screen.fill(BLACK) #clear previous frame
        pygame.time.delay(300)
        print "updating..."
        tm.updateTiles()
        pygame.display.flip()
