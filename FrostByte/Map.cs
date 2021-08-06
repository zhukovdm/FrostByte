using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace FrostByte
{
    class Level
    {
        public int     order;
        public int     height;
        public int     width;
        public char[,] table;
        public List<MovingPiece> npcs;
        public List<MovingPiece> bullets;

        public Level heroLevel;
        public int   heroRow;
        public int   heroCol;

        public Level(int order, int height, int width)
        {
            this.order = order;
            this.height = height;
            this.width = width;
            table = new char[height, width];
            npcs = new List<MovingPiece>();
            bullets = new List<MovingPiece>();
        }
    }

    class Map
    {
        public Random rnd;
        public State state;
        public Buttons buttons;
        
        public int HeartsCount;
        public int FriendsCount;
        public int BulletsCount;
        public int CurrentLevel;
        public int LevelCount;

        Graphics graphics;
        int pixelSize;
        string strIcons;
        Bitmap[] bmpIcons;
        Level[] plan;
        Level level;
        Hero hero;
        bool levelTransition;

        public Map(Graphics graphics, Buttons buttons)
        {
            rnd = new Random(12345);
            state = State.NotStarted;
            this.graphics = graphics;
            this.buttons = buttons;
            HeartsCount = 3;
            FriendsCount = 0;
            BulletsCount = 0;
            levelTransition = false;
            readIcons();
            readPlan();
            state = State.InProgress;
        }

        public bool IsVacant(int row, int col) => level.table[row, col] == ' ';

        public bool IsWall(int row, int col) => level.table[row, col] == '.';

        public bool IsLevel(int row, int col)
            => "01234".IndexOf(level.table[row, col]) >= 0;

        public bool IsHero(int row, int col)
            => (level.table[row, col] == 'D') || (level.table[row, col] == 'E');

        public bool IsHeart(int row, int col) => level.table[row, col] == 'A';

        public bool IsFriend(int row, int col) => level.table[row, col] == 'B';

        public bool IsLargeBullet(int row, int col) => level.table[row, col] == 'C';

        public bool IsSmallBullet(int row, int col) => level.table[row, col] == 'F';

        public bool IsTreasure(int row, int col)
            => IsHeart(row, col) || IsFriend(row, col) || IsLargeBullet(row, col);

        public bool IsStone(int row, int col)
            => IsWall(row, col) || IsTreasure(row, col) || IsLevel(row, col);

        public bool IsNpc(int row, int col)
            => "TUVWXYZ^v<>".IndexOf(level.table[row, col]) >= 0;

        public void SetPieceView(MovingPiece piece, int row, int col, char view)
        {
            if (piece.row == row && piece.col == col) {
                level.table[row, col] = view;
            }
        }

        public void AddBullet(int row, int col, int row_dir, int col_dir)
        {
            // hit npc immediately
            if (IsNpc(row, col)) {
                foreach (var npc in level.npcs) {
                    if (npc.row == row && npc.col == col) {
                        CancelMovingPiece(npc);
                    }
                }
                return;
            }

            // otherwise create moving bullet
            level.table[row, col] = 'F';
            var bullet = new Bullet(this, row, col, row_dir, col_dir);
            level.bullets.Add(bullet);
        }

        private void RemoveMarkedPieces()
        {
            int bound;

            // safe iteration end ~> begin
            bound = level.npcs.Count;
            for (int i = bound - 1; i >= 0; i--) {
                if (level.npcs[i].cancelled) {
                    level.npcs.RemoveAt(i);
                }
            }

            bound = level.bullets.Count;
            for (int i = bound - 1; i >= 0; i--) {
                if (level.bullets[i].cancelled) {
                    level.bullets.RemoveAt(i);
                }
            }
        }

        public void MovePiece(MovingPiece piece, int row_new, int col_new)
        {
            var view = level.table[piece.row, piece.col];
            level.table[piece.row, piece.col] = ' ';
            level.table[row_new, col_new] = view;
            piece.row = row_new;
            piece.col = col_new;
        }

        public void HeroDiscoversTreasure(int row, int col)
        {
            switch (level.table[row, col]) {
                case 'A':
                    HeartsCount++;
                    break;
                case 'B':
                    FriendsCount--;
                    break;
                case 'C':
                    BulletsCount += 7;
                    break;
                default:
                    break;
            }
        }

        public void MoveHero(int row_new, int col_new)
        {
            char view = level.table[hero.row, hero.col];
            level.table[hero.row, hero.col] = ' ';

            if (hero.jump || (hero.fall && hero.col == col_new)) {
                view = 'D'; // ^v
            } else {
                view = hero.view[1 - hero.view.IndexOf(view)]; // <>
            }

            HeroDiscoversTreasure(row_new, col_new);
            level.table[row_new, col_new] = view;
            hero.row = row_new;
            hero.col = col_new;
        }

        public void CancelMovingPiece(MovingPiece piece)
        {
            piece.cancelled = true;
            level.table[piece.row, piece.col] = ' ';
        }

        public void CollisionNpcBullet(MovingPiece npc)
        {
            CancelMovingPiece(npc);
            foreach (var bullet in level.bullets) {
                if (bullet.row == npc.row + npc.row_dir && bullet.col == npc.col + npc.col_dir) {
                    bullet.cancelled = true;
                    break;
                }
            }
        }

        public void CollisionNpcHero(MovingPiece npc)
        {
            CancelMovingPiece(npc);
            HeartsCount--;
        }

        public void CollisionBulletNpc(Bullet bullet)
        {
            CancelMovingPiece(bullet);

            foreach (var npc in level.npcs) {
                if (!npc.cancelled &&
                    npc.row == bullet.row + bullet.row_dir &&
                    npc.col == bullet.col + bullet.col_dir) {
                    CancelMovingPiece(npc);
                    break;
                }
            }
        }

        public void CollisionHeroNpc(int row_new, int col_new)
        {
            foreach (var npc in level.npcs) {
                if (npc.row == row_new && npc.col == col_new) {
                    CollisionNpcHero(npc);
                    break;
                }
            }
            
            MoveHero(row_new, col_new);
        }

        public bool TryMoveNPC(MovingPiece npc, int row_new, int col_new)
        {
            if (IsSmallBullet(row_new, col_new)) {
                CollisionNpcBullet(npc);
                return true;
            } else if (IsHero(row_new, col_new)) {
                CollisionNpcHero(npc);
                return true;
            } else if (IsVacant(row_new, col_new)) {
                MovePiece(npc, row_new, col_new);
                return true;
            }
            return false;
        }

        public bool TryMoveBullet(Bullet bullet, int row_new, int col_new)
        {
            if (IsWall(row_new, col_new) || IsLevel(row_new, col_new) ||
                IsTreasure(row_new, col_new) || bullet.cancelled) {
                CancelMovingPiece(bullet);
            } else if (IsNpc(row_new, col_new)) {
                CollisionBulletNpc(bullet);
            } else {
                MovePiece(bullet, row_new, col_new);
            }

            return true;
        }

        private void CalculateCoordinates(int newA, int newB, int dim1, int dim2, bool rc)
        {
            int tempA, tempB, tempO;
            tempO = 0;
            int view;

            // determine order of window on the current level
            tempB = -1;
            while (tempB < newB) {
                tempB++;
                view = rc ? level.table[newA, tempB]
                          : level.table[tempB, newA];
                if (view - '0' == level.heroLevel.order) {
                    tempO++;
                }
            }

            // extremal on initial, opposite on target level
            tempA = (dim1 - (newA + 1) % dim1) % dim1;
            tempA = (tempA == 0) ? 0 : dim2 - 1;

            // calculate coordinates of the window on the target level
            tempB = -1;
            while (tempO > 0) {
                tempB++;
                view = rc ? level.heroLevel.table[tempA, tempB]
                          : level.heroLevel.table[tempB, tempA];
                if (view - '0' == level.order) {
                    tempO--;
                }
            }

            // coordinates where the hero will be placed
            int where = (tempA == 0) ? 1 : -1;
            level.heroLevel.heroRow = rc ? tempA + where : tempB;
            level.heroLevel.heroCol = rc ? tempB : tempA + where;
        }

        public void MoveHeroToLevel(int row_new, int col_new)
        {
            levelTransition = true;
            level.heroLevel = plan[level.table[row_new, col_new] - '0'];

            // ^v
            if (row_new == 0 || row_new == level.height - 1) {
                CalculateCoordinates(row_new, col_new, level.height, level.heroLevel.height, true);
            }

            // <>
            else if (col_new == 0 || col_new == level.width - 1) {
                CalculateCoordinates(col_new, row_new, level.width, level.heroLevel.width, false);
            }
            
            // ?
            else {
                throw new Exception("Invalid level transition."); 
            }
        }

        public void MoveToLevel()
        {
            char view = level.table[hero.row, hero.col];
            level.table[hero.row, hero.col] = ' ';
            level = level.heroLevel;

            if (IsTreasure(level.heroRow, level.heroCol)) {
                HeroDiscoversTreasure(level.heroRow, level.heroCol);
            } else if (IsNpc(level.heroRow, level.heroCol)) {
                foreach (var npc in level.npcs) {
                    if (npc.row == level.heroRow && npc.col == level.heroCol) {
                        CancelMovingPiece(npc);
                        HeartsCount--;
                        break;
                    }
                }
            }

            hero.row = level.heroRow;
            hero.col = level.heroCol;
            level.table[level.heroRow, level.heroCol] = view;
            RemoveMarkedPieces();
            CurrentLevel = level.order;
        }

        private void readIcons()
        {
            var f = new StreamReader("Resources\\Icons.txt");
            strIcons = f.ReadLine();
            f.Close();

            int cnt = strIcons.Length;
            bmpIcons = new Bitmap[cnt];
            Bitmap bmp = new Bitmap("Resources\\Icons.png");

            pixelSize = bmp.Height;
            for (int i = 0; i < cnt; i++) {
                Rectangle rect = new Rectangle(i * pixelSize, 0, pixelSize, pixelSize);
                bmpIcons[i] = bmp.Clone(rect, System.Drawing.Imaging.PixelFormat.DontCare);
            }
        }

        private void readPlan()
        {
            var file = "Resources\\Plan.txt";
            StreamReader f = new StreamReader(file);
            int cnt = int.Parse(f.ReadLine()) + 1;
            plan = new Level[cnt];
            var dir = new int[] { -1, 1 };

            for (int i = 0; i < cnt; i++) {
                int height = int.Parse(f.ReadLine());
                int width = int.Parse(f.ReadLine());
                plan[i] = new Level(i, height, width);
                for (int row = 0; row < height; row++) {
                    string line = f.ReadLine();
                    for (int col = 0; col < width; col++) {
                        char letter = line[col];
                        plan[i].table[row, col] = letter;
                        switch (line[col]) {
                            case 'B':
                                FriendsCount++;
                                break;
                            case 'D':
                            case 'E':
                                hero = new Hero(this, row, col);
                                level = plan[i];
                                CurrentLevel = i;
                                break;
                            case 'T':
                                plan[i].npcs.Add(new Tongue(this, row, col, 0, dir[rnd.Next(dir.Length)]));
                                break;
                            case 'U':
                                plan[i].npcs.Add(new Triangle(this, row, col, 0, dir[rnd.Next(dir.Length)]));
                                break;
                            case 'V':
                                plan[i].npcs.Add(new Gremlin(this, row, col, dir[rnd.Next(dir.Length)], 0));
                                break;
                            case 'W':
                                plan[i].npcs.Add(new WaterDrop(this, row, col, 1, 0));
                                break;
                            case 'X':
                                plan[i].npcs.Add(new Alien(this, row, col, dir[rnd.Next(dir.Length)], dir[rnd.Next(dir.Length)]));
                                break;
                            case 'Y':
                                plan[i].npcs.Add(new Jellyfish(this, row, col));
                                break;
                            case 'Z':
                                plan[i].npcs.Add(new Skull(this, row, col));
                                break;
                            case '^':
                            case 'v':
                            case '<':
                            case '>':
                                plan[i].npcs.Add(new Monster(this, row, col, line[col]));
                                break;
                            default:
                                break;
                        }
                    }
                }
                f.ReadLine();
            }
            f.Close();
            LevelCount = plan.Length;
        }

        public void MoveAllPieces()
        {
            foreach (var npc in level.npcs) { npc.Move(); }
            foreach (var bullet in level.bullets) { bullet.Move(); }

            hero.Move();
            RemoveMarkedPieces();

            if (levelTransition) {
                Clear();
                MoveToLevel();
                levelTransition = false;
            }

            if (FriendsCount <= 0) {
                state = State.Win;
            }
            if (HeartsCount <= 0) {
                state = State.Lose;
            }
        }

        public void Draw()
        {
            for (int row = 0; row < level.height; row++) {
                for (int col = 0; col < level.width; col++) {
                    int iconIndex = strIcons.IndexOf(level.table[row, col]);
                    graphics.DrawImage(bmpIcons[iconIndex], col * pixelSize, row * pixelSize);
                }
            }
        }

        public void Clear()
        {
            int iconIndex = strIcons.IndexOf(' ');
            for (int row = 0; row < level.height; row++) {
                for (int col = 0; col < level.width; col++) {
                    graphics.DrawImage(bmpIcons[iconIndex], col * pixelSize, row * pixelSize);
                }
            }
        }
    }
}
