namespace FrostByte
{
    abstract class Piece { }

    abstract class MovingPiece : Piece
    {
        public Map map;
        public int row, col, row_dir, col_dir;
        public int row_new, col_new;
        public bool cancelled = false;

        public MovingPiece(Map map, int row, int col, int row_dir, int col_dir)
		{
            this.map = map;
            this.row = row;
            this.col = col;
            this.row_dir = row_dir;
            this.col_dir = col_dir;
        }

        public abstract void GetNewPosition();
        public abstract void Move();
    }

    class Tongue : MovingPiece
    {
        public Tongue(Map map, int row, int col, int row_dir, int col_dir)
            : base(map, row, col, row_dir, col_dir) { }

        public override void GetNewPosition()
        {
            row_new = row + row_dir;
            col_new = col + col_dir;
        }

        public override void Move()
        {
            GetNewPosition();

            // stone is in front, change direction
            if (!map.TryMoveNPC(this, row_new, col_new)) {
                row_dir *= -1;
                col_dir *= -1;
            }
        }
    }

    class Gremlin : Tongue
    {
        public Gremlin(Map map, int row, int col, int row_dir, int col_dir)
            : base(map, row, col, row_dir, col_dir) { }
    }

    class Triangle : MovingPiece
    {
        private int row_adj, col_adj;

        public Triangle(Map map, int row, int col, int row_dir, int col_dir)
            : base(map, row, col, row_dir, col_dir) { }

        public override void GetNewPosition()
        {
            row_new = row + row_dir;
            col_new = col + col_dir;
        }

        public override void Move()
        {
            GetNewPosition();

            // stone is found
            if (!map.TryMoveNPC(this, row_new, col_new))
            {
                row_adj = -row_dir;
                col_adj = -col_dir;

                // find opposite stone and move there
                while (!map.IsStone(row + row_adj, col + col_adj))
                {
                    row_adj -= row_dir;
                    col_adj -= col_dir;
                }

                // one step back
                _ = map.TryMoveNPC(this, row + row_adj + row_dir, col + col_adj + col_dir);
            }
        }
    }

    class WaterDrop : Triangle
    {
        public WaterDrop(Map map, int row, int col, int row_dir, int col_dir)
            : base(map, row, col, row_dir, col_dir) { }
    }

    class Alien : MovingPiece
    {
        public Alien(Map map, int row, int col, int row_dir, int col_dir)
            : base(map, row, col, row_dir, col_dir) { }

        public override void GetNewPosition()
        {
            // corner reflection
            if (map.IsStone(row + row_dir, col) && map.IsStone(row, col + col_dir)) {
                row_dir *= -1;
                col_dir *= -1;
            }

            // row reflection
            else if (map.IsStone(row + row_dir, col) && !map.IsStone(row, col + col_dir)) {
                row_dir *= -1;
            }
            
            // column reflection
            else if (!map.IsStone(row + row_dir, col) && map.IsStone(row, col + col_dir)) {
                col_dir *= -1;
            }
            
            // no reflection
            else { }

            row_new = row + row_dir;
            col_new = col + col_dir;
        }

        public override void Move()
        {
            GetNewPosition();
            if (!map.TryMoveNPC(this, row_new, col_new)) {
                row_dir *= -1;
                col_dir *= -1;
            }
        }
    }

    class Jellyfish : MovingPiece
    {
        int[] dir = { -1, 0, 1 };

        public Jellyfish(Map map, int row, int col)
            : base(map, row, col, 0, 0) { }

        public override void GetNewPosition()
        {
            row_new = row + dir[map.rnd.Next(dir.Length)];
            col_new = col + dir[map.rnd.Next(dir.Length)];
        }

        public override void Move()
        {
            GetNewPosition();
            map.TryMoveNPC(this, row_new, col_new);
        }
    }

    class Skull : MovingPiece
    {
        double bound = 0.2;

        public Skull(Map map, int row, int col)
            : base(map, row, col, 0, 0) { }

        public override void GetNewPosition()
        {
            row_new = row;
            col_new = col;
            for (int row_dir = -1; row_dir < 2; row_dir++) {
                for (int col_dir = -1; col_dir < 2; col_dir++) {
                    if (map.IsHero(row + row_dir, col + col_dir) ||
                        map.IsSmallBullet(row + row_dir, col + col_dir)) {
                        if (map.rnd.NextDouble() < bound) {
                            map.CollisionNpcHero(this);
                        }
                    }
                }
            }
        }

        public override void Move() => GetNewPosition();
    }

    class Monster : MovingPiece
    {
        int row_right, col_right; // smer doprava
        string views;
        public char view;
        bool rot;

        public Monster(Map map, int row, int col, char view)
            : base(map, row, col, 0, 0)
        {
            views = ">^<v";
            this.view = view;
            rot = false;

            switch (view) {
                case '>':
                    row_dir = 0;
                    col_dir = 1;
                    row_right = 1;
                    col_right = 0;
                    break;
                case '^':
                    row_dir = -1;
                    col_dir = 0;
                    row_right = 0;
                    col_right = 1;
                    break;
                case '<':
                    row_dir = 0;
                    col_dir = -1;
                    row_right = -1;
                    col_right = 0;
                    break;
                case 'v':
                    row_dir = 1;
                    col_dir = 0;
                    row_right = 0;
                    col_right = -1;
                    break;
                default:
                    break;
            }
        }

        private void swapDirections(ref int r1, ref int s1, ref int r2, ref int s2)
        {
            // 1 -> 2, 2 -> 1
            int temp_r = r1;
            int temp_s = s1;
            r1 = -r2;
            s1 = -s2;
            r2 = temp_r;
            s2 = temp_s;
        }

        private void rotate(ref int r1, ref int s1, ref int r2, ref int s2, int dir)
        {
            swapDirections(ref r1, ref s1, ref r2, ref s2);
            view = views[(views.IndexOf(view) + dir + views.Length) % views.Length];
            map.SetPieceView(this, row, col, view);
        }

        public override void GetNewPosition()
        {
            row_new = row;
            col_new = col;

            // stone is under the monster
            if (map.IsStone(row + row_right, col + col_right)) {
                
                // stone is found in front -> rotate
                if (map.IsStone(row + row_dir, col + col_dir)) {
                    rotate(ref row_dir, ref col_dir, ref row_right, ref col_right, 1);
                }
                
                // no stone -> continue forward
                else {
                    row_new = row + row_dir;
                    col_new = col + col_dir;
                }
            }
            
            // no stone under the monster
            else {
                
                // at least one rotation
                if (!rot) {
                    rot = true;
                    rotate(ref row_right, ref col_right, ref row_dir, ref col_dir, -1);
                }

                // looking for the diagonal stone
                else {
                    if (map.IsStone(row + row_dir + row_right, col + col_dir + col_right) &&
                        !map.IsNpc(row + row_dir, col + col_dir)) {
                        rot = false;
                        row_new = row + row_dir;
                        col_new = col + col_dir;
                    }
                    
                    // rotate once again
                    else {
                        rotate(ref row_right, ref col_right, ref row_dir, ref col_dir, -1);
                    }
                }
            }
        }

        public override void Move()
        {
            GetNewPosition();
            if (row_new != row || col_new != col) {
                map.TryMoveNPC(this, row_new, col_new);
            }
        }
    }

    class Bullet : MovingPiece
    {
        public Bullet(Map map, int row, int col, int row_dir, int col_dir)
            : base(map, row, col, row_dir, col_dir) { }

        public override void GetNewPosition()
        {
            row_new = row + row_dir;
            col_new = col + col_dir;
        }

        public override void Move()
        {
            GetNewPosition();
            map.TryMoveBullet(this, row_new, col_new);
        }
    }

    class Hero : MovingPiece
    {
        public string view;
        public bool jump, fall;

        public Hero(Map map, int row, int col)
            : base(map, row, col, 0, 0)
        {
            view = "DE";
            jump = fall = false;
        }

        public override void GetNewPosition()
        {
            row_new = row;
            col_new = col;
            
            // launch jump
            if (map.buttons.Up && !jump && !fall) {
                jump = true;
            }

            // roof
            if (jump && map.IsWall(row - 1, col)) {
                jump = false;
            }

            // move left -> jump is interrupted
            if (map.buttons.Left) {
                col_new--;
                jump = false;
            }

            // move right -> jump is interrupted
            if (map.buttons.Right) {
                col_new++;
                jump = false;
            }

            // no jump and no wall under -> fall
            if (!jump && !map.IsWall(row + 1, col)) {
                fall = true;
            }

            // floor -> stop falling
            if (fall && map.IsWall(row + 1, col)) {
                fall = false;
            }

            // inc or dec iff jump or fall
            if (jump) {
                row_new--;
            } else if (fall) {
                row_new++;
            }
        }

        private void isFire()
        {
            int row_bullet = row;
            int col_bullet = col;

            // buttons
            if (map.buttons.W) { row_bullet--; }
            if (map.buttons.S) { row_bullet++; }
            if (map.buttons.A) { col_bullet--; }
            if (map.buttons.D) { col_bullet++; }

            // new bullet and hero don't collide
            if ((row_bullet != row || col_bullet != col) &&
                (row_bullet != row_new || col_bullet != col_new)) {
                map.HeroBullets--;
                if (map.IsNpc(row_bullet, col_bullet) || map.IsVacant(row_bullet, col_bullet)) {
                    map.AddBullet(row_bullet, col_bullet, row_bullet - row, col_bullet - col);
                }
            }
        }

        public override void Move()
        {
            GetNewPosition();

            // hero has bullets -> check if fire
            if (map.HeroBullets > 0) { isFire(); }

            if (row_new != row || col_new != col) {
                if (map.IsVacant(row_new, col_new) || map.IsTreasure(row_new, col_new)) {
                    map.MoveHero(row_new, col_new);
                } else if (map.IsNpc(row_new, col_new)) {
                    map.KolizeHrdinaNPC(row_new, col_new);
                } else if (map.IsLevel(row_new, col_new)) {
                    map.HrdinaNaJinouUroven(row_new, col_new);
                } else {
                    // wall, do nothing
                }
            }
        }
    }
}
