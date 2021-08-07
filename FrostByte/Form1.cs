using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FrostByte
{
    public partial class Form1 : Form
    {
        Map map;
        Graphics graphics;
        Buttons buttons;
        List<Control> cIntro;
        List<Control> cGame;
        List<Control> cPause;
        List<Control> cEnd;
        List<Timer> timers;

        int tmInterval = 200;

        public Form1()
        {
            InitializeComponent();

            cIntro = new List<Control>() {
                pbIntro, lbContinue
            };

            cGame = new List<Control>() {
                pbHeart, lbHeart,
                pbFriend, lbFriend,
                pbBullet, lbBullet,
                lbLevels
            };

            cPause = new List<Control>() {
                lbPause
            };

            cEnd = new List<Control>() {
                lbRepeat, lbWinLose
            };

            timers = new List<Timer>() {
                tmIntro, tmGame, tmPause, tmEnd
            };
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            buttons = new Buttons();

            foreach (var timer in timers) {
                timer.Enabled = false;
                timer.Interval = tmInterval;
            }

            foreach (var control in cIntro) {
                control.Visible = true;
            }

            foreach (var control in cGame) {
                control.Visible = false;
            }

            foreach (var control in cPause) {
                control.Visible = false;
            }

            foreach (var control in cEnd) {
                control.Visible = false;
            }

            tmIntro.Enabled = true;
        }

        protected override void OnKeyDown(KeyEventArgs e) {
            base.OnKeyDown(e);
            switch (e.KeyCode) {
                case Keys.Escape:
                    Application.Exit();
                    break;
                case Keys.Enter:
                    buttons.Enter = true;
                    break;
                case Keys.Up:
                    buttons.Up = true;
                    break;
                case Keys.Down:
                    buttons.Down = true;
                    break;
                case Keys.Left:
                    buttons.Left = true;
                    break;
                case Keys.Right:
                    buttons.Right = true;
                    break;
                case Keys.W:
                    buttons.W = true;
                    break;
                case Keys.S:
                    buttons.S = true;
                    break;
                case Keys.A:
                    buttons.A = true;
                    break;
                case Keys.D:
                    buttons.D = true;
                    break;
                case Keys.Pause:
                    buttons.Pause = 1 - buttons.Pause;
                    break;
                default:
                    break;
            }
        }

        protected override void OnKeyUp(KeyEventArgs e) {
            base.OnKeyUp(e);
            switch (e.KeyCode) {
                case Keys.Enter:
                    buttons.Enter = false;
                    break;
                case Keys.Up:
                    buttons.Up = false;
                    break;
                case Keys.Down:
                    buttons.Down = false;
                    break;
                case Keys.Left:
                    buttons.Left = false;
                    break;
                case Keys.Right:
                    buttons.Right = false;
                    break;
                case Keys.W:
                    buttons.W = false;
                    break;
                case Keys.S:
                    buttons.S = false;
                    break;
                case Keys.A:
                    buttons.A = false;
                    break;
                case Keys.D:
                    buttons.D = false;
                    break;
                default:
                    break;
            }
        }

        private void tmIntro_Tick(object sender, EventArgs e)
        {
            if (buttons.Enter) {
                tmIntro.Enabled = false;

                foreach (var control in cIntro) {
                    control.Visible = false;
                }

                foreach (var control in cGame) {
                    control.Visible = true;
                }

                graphics = CreateGraphics();
                map = new Map(graphics, buttons);
                tmGame.Enabled = true;
            }
        }

        private void tmGame_Tick(object sender, EventArgs e)
        {
            if (buttons.Pause == 1) {
                tmGame.Enabled = false;
                lbPause.Visible = true;
                tmPause.Enabled = true;
            } else {
                switch (map.state) {

                    case State.InProgress:
                        map.MoveAllPieces();
                        map.Draw();
                        lbHeart.Text = map.HeartsCount.ToString();
                        lbFriend.Text = map.FriendsCount.ToString();
                        lbBullet.Text = map.BulletsCount.ToString();
                        lbLevels.Text = map.CurrentLevel.ToString() + "/" +
                                        (map.LevelCount - 1).ToString();
                        break;

                    case State.Win:
                    case State.Lose:
                        tmGame.Enabled = false;
                        map.Clear();

                        var winLose = map.state == State.Win ? "win" : "lose";
                        lbWinLose.Text = "You " + winLose + "!";

                        foreach (var control in cGame) {
                            control.Visible = false;
                        }

                        foreach (var control in cEnd) {
                            control.Visible = true;
                        }

                        tmEnd.Enabled = true;
                        break;

                    default:
                        break;
                }
            }
        }

        private void tmPause_Tick(object sender, EventArgs e)
        {
            if (buttons.Pause == 0) {
                tmGame.Enabled = true;
                lbPause.Visible = false;
                tmPause.Enabled = false;
            }
        }

        private void tmEnd_Tick(object sender, EventArgs e)
        {
            if (buttons.Enter) {
                tmEnd.Enabled = false;

                foreach (var control in cEnd) {
                    control.Visible = false;
                }

                foreach (var control in cGame) {
                    control.Visible = true;
                }

                graphics = CreateGraphics();
                map = new Map(graphics, buttons);
                tmGame.Enabled = true;
            }
        }
    }
}
