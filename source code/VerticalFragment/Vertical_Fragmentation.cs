using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VerticalFragment
{
    public partial class UI_MainForm : Form
    {
        static int UseCol, UseRow = 0; //number of Column and Row of Usage matrix
        static int AFCol, AFRow = 0;//number of Column and Row of Access Frequency matrix
        static int AACol, AARow = 0;//number of Column and Row of Attribute Affinity matrix
        static int CACol, CARow = 0;//number of Column and Row of Clustered Affinity matrix

        static int AAvalue = 0; //Create temp value to store AA matrix values

        static int[,] matrix_Usage = new int[1, 1]; //Array to store Usage matrix value
        static int[,] matrix_tempUsage = new int[1, 1];
        static int[,] matrix_AF = new int[1, 1]; //Array to store Access Frequency matrix value
        static int[,] matrix_AA = new int[1, 1]; //Array to store Access Frequency matrix value
        static int[,] matrix_CA = new int[1, 1]; //Array to store Cluster Affinity matrix value
        static int[,] matrix_temp1 = new int[1, 1]; //Array to store Temporary matrix
        static int[,] matrix_temp2 = new int[1, 1];
        static int[,] matrix_temp3 = new int[1, 1];
        

        int TotalUse = 0; //Total number in Usage matrix
        int TotalAF = 0; //Total number in Access Frequency matrix

        static int[] CAPosition = new int[1];

        public UI_MainForm()
        {
            InitializeComponent();
            MessageBox.Show("Please Open in full Window!!!");
            btn_AFSave.Enabled = false;
            btn_UseSave.Enabled = false;
        }



        //====================================================================================================================

        //Create Dynamic textbox to input Usage matrix
        private void btn_AAGenerate_Click(object sender, EventArgs e)
        {
            //Must have more than 2 column
            if (Convert.ToInt32(tbx_UseCol.Text) > 2 && Convert.ToInt32(tbx_UseCol.Text) <=10)
            {
                btn_UseSave.Enabled = true;
                pnl_UseMatrix.Controls.Clear();
                int PointX = 35; //Align from Left for textbox
                int PointY = 30; //Align from top for textbox
                int att = 1; //No. of Attribute lable
                int qr = 1; //No. of query lable
                int name = 1; //No. of textbox
                TotalUse = Convert.ToInt32(tbx_UseRow.Text) * Convert.ToInt32(tbx_UseCol.Text); //Total value of Use matrix
                tbx_AFRow.Text = tbx_UseRow.Text;

                for (UseRow = 0; UseRow < Convert.ToInt32(tbx_UseRow.Text); UseRow++)
                {
                    PointX = 35;

                    Label UseQuery = new Label();
                    UseQuery.Text = ("Q" + (qr++)).ToString();
                    UseQuery.Location = new Point(5, PointY + 5);
                    UseQuery.Size = new Size(30, 30);
                    pnl_UseMatrix.Controls.Add(UseQuery);
                    pnl_UseMatrix.Show();

                    for (UseCol = 0; UseCol < Convert.ToInt32(tbx_UseCol.Text); UseCol++)
                    {
                        TextBox Usetbx = new TextBox();
                        Usetbx.Size = new Size(30, 30);
                        Usetbx.Location = new Point(PointX, PointY);
                        Usetbx.Name = "tbx_UseValue" + name.ToString();
                        pnl_UseMatrix.Controls.Add(Usetbx);
                        pnl_UseMatrix.Show();

                        if (UseRow == 0)
                        {
                            Label Useattribute = new Label();
                            Useattribute.Text = ("A" + att).ToString();
                            Useattribute.Location = new Point(PointX + 5, 5);
                            Useattribute.Size = new Size(30, 30);
                            pnl_UseMatrix.Controls.Add(Useattribute);
                            pnl_UseMatrix.Show();
                        }
                        att++;
                        PointX += 35;
                        name++;
                    }

                    PointY += 30;
                }
            }
            else
                MessageBox.Show("The number of column must larger than 2 or less than 11!!!!");

        }


        //Get value for Usage Matrix and tempUsage Matrix
        private void btn_UseSave_Click(object sender, EventArgs e)
        {
            matrix_Usage = new int[Convert.ToInt32(tbx_UseRow.Text), Convert.ToInt32(tbx_UseCol.Text)];
            matrix_tempUsage = new int[Convert.ToInt32(tbx_UseRow.Text), Convert.ToInt32(tbx_UseCol.Text)];
            int i = 1;
            for (UseRow = 0; UseRow < Convert.ToInt32(tbx_UseRow.Text); UseRow++)
            {
                for (UseCol = 0; UseCol < Convert.ToInt32(tbx_UseCol.Text); UseCol++)
                {
                    matrix_Usage[UseRow, UseCol] = Convert.ToInt32(((TextBox)pnl_UseMatrix.Controls["tbx_UseValue" + i.ToString()]).Text);
                    matrix_tempUsage[UseRow, UseCol] = Convert.ToInt32(((TextBox)pnl_UseMatrix.Controls["tbx_UseValue" + i.ToString()]).Text);
                    i++;
                }
            }
            MessageBox.Show("Add to Usage Matrix sucess!!!");

        }



        //====================================================================================================================

        //Create Dynamic textbox to input Access Frequency matrix
        private void bnt_AFGenerate_Click(object sender, EventArgs e)
        {
            btn_AFSave.Enabled = true;
            pnl_AFMatrix.Controls.Clear();
            int PointX = 35; //Align from Left for textbox
            int PointY = 30; //Align from top for textbox
            int site = 1; //No. of Attribute lable
            int qr = 1; //No. of query lable
            int name = 1; //No. of textbox
            TotalAF = Convert.ToInt32(tbx_AFRow.Text) * Convert.ToInt32(tbx_AFCol.Text);
            PointX = 35;

            for (AFRow = 0; AFRow < Convert.ToInt32(tbx_AFRow.Text); AFRow++)
            {
                PointX = 35;

                Label AFQuery = new Label();
                AFQuery.Text = ("Q" + (qr++)).ToString();
                AFQuery.Location = new Point(5, PointY + 5);
                AFQuery.Size = new Size(30, 30);
                pnl_AFMatrix.Controls.Add(AFQuery);
                pnl_AFMatrix.Show();

                for (AFCol = 0; AFCol < Convert.ToInt32(tbx_AFCol.Text); AFCol++)
                {
                    TextBox AFtbx = new TextBox();
                    AFtbx.Size = new Size(30, 30);
                    AFtbx.Location = new Point(PointX, PointY);
                    AFtbx.Name = "tbx_AFValue" + name.ToString();
                    pnl_AFMatrix.Controls.Add(AFtbx);
                    pnl_AFMatrix.Show();

                    if (AFRow == 0)
                    {
                        Label AFattribute = new Label();
                        AFattribute.Text = ("S" + site).ToString();
                        AFattribute.Location = new Point(PointX + 5, 5);
                        AFattribute.Size = new Size(30, 30);
                        pnl_AFMatrix.Controls.Add(AFattribute);
                        pnl_AFMatrix.Show();
                    }
                    site++;
                    PointX += 35;
                    name++;
                }

                PointY += 30;
            }

            PointY += 30;
        }

        //Get value for Access Frequency Matrix
        private void btn_AFSave_Click(object sender, EventArgs e)
        {
            matrix_AF = new int[Convert.ToInt32(tbx_AFRow.Text), Convert.ToInt32(tbx_AFCol.Text)];
            int i = 1;
            for (AFRow = 0; AFRow < Convert.ToInt32(tbx_AFRow.Text); AFRow++)
            {
                for (AFCol = 0; AFCol < Convert.ToInt32(tbx_AFCol.Text); AFCol++)
                {
                    matrix_AF[AFRow, AFCol] = Convert.ToInt32(((TextBox)pnl_AFMatrix.Controls["tbx_AFValue" + i.ToString()]).Text);
                    i++;
                }
                AFCol = 0;
            }
            MessageBox.Show("Add to Access Frequency Matrix sucess!!!");
        }




        //====================================================================================================================

        //Confirm to find vertical fragmentation
        private void btn_Confirm_Click(object sender, EventArgs e)
        {
            pnl_AAMatrix.Controls.Clear();
            pnl_CAMatrix.Controls.Clear();
            pnl_tempUseMatrix.Controls.Clear();
            rtbx_AAMatrix.Text = "Solution:";
            rtbx_CAMatrix.Text = "Solution:";
            rtbx_VF.Text = "Solution:";

            int VerAtt = 1;
            int HorAtt = 1;
            int PointX = 35;
            int PointY = 30;




            //=============================Calculate AA Matrix=========================

            //Create AA Matrix
            matrix_AA = new int[Convert.ToInt32(tbx_UseCol.Text), Convert.ToInt32(tbx_UseCol.Text)];

            //Insert values to AA Matrix
            for (AARow = 0; AARow < Convert.ToInt32(tbx_UseCol.Text); AARow++)
            {
                PointX = 35;

                Label HorAAattribute = new Label();
                HorAAattribute.Text = ("A" + HorAtt).ToString();
                HorAAattribute.Location = new Point(5, PointY + 5);
                HorAAattribute.Size = new Size(30, 30);
                pnl_AAMatrix.Controls.Add(HorAAattribute);
                pnl_AAMatrix.Show();

                for (AACol = 0; AACol < Convert.ToInt32(tbx_UseCol.Text); AACol++)
                {
                    rtbx_AAMatrix.AppendText(Environment.NewLine + " AA[" + AARow + "][" + AACol + "]: 0");

                    AAvalue = 0;
                    for (int Rowtemp = 0; Rowtemp < Convert.ToInt32(tbx_UseRow.Text); Rowtemp++)
                    {
                        if (matrix_Usage[Rowtemp, AACol] == 1 && matrix_Usage[Rowtemp, AARow] == 1)
                        {
                            for (AFCol = 0; AFCol < Convert.ToInt32(tbx_AFCol.Text); AFCol++)
                            {

                                AAvalue = AAvalue + matrix_AF[Rowtemp, AFCol];
                                rtbx_AAMatrix.Text = rtbx_AAMatrix.Text + " + " + matrix_AF[Rowtemp, AFCol] ;
                            }
                        }
                    }

                    rtbx_AAMatrix.Text = rtbx_AAMatrix.Text + " = " + AAvalue;
                    matrix_AA[AARow, AACol] = AAvalue;

                    Label AAattribute = new Label();
                    AAattribute.Text = AAvalue.ToString();
                    AAattribute.Location = new Point(PointX, PointY + 5);
                    AAattribute.Size = new Size(30, 30);
                    pnl_AAMatrix.Controls.Add(AAattribute);
                    pnl_AAMatrix.Show();

                    if (AARow == 0)
                    {
                        Label VerAAattribute = new Label();
                        VerAAattribute.Text = ("A" + VerAtt).ToString();
                        VerAAattribute.Location = new Point(PointX + 5, 5);
                        VerAAattribute.Size = new Size(30, 30);
                        pnl_AAMatrix.Controls.Add(VerAAattribute);
                        pnl_AAMatrix.Show();
                    }
                    VerAtt++;
                    PointX += 35;
                }
                HorAtt++;
                PointY += 30;
            }



            //=============================Calculate CA Matrix=========================

            //reset Position
            VerAtt = 1;
            HorAtt = 1;
            PointX = 35;
            PointY = 30;

            //Create CA Matrix and copy temp value
            CACol = Convert.ToInt32(tbx_UseCol.Text);
            CARow = Convert.ToInt32(tbx_UseCol.Text);
            matrix_CA = new int[CARow, CACol];
            CAPosition = new int[CARow];

            //Copy temp value from AAMatrix to CAMatrix
            for (int row = 0; row < CARow; row++)
            {
                CAPosition[row] = (row + 1);
                for (int col = 0; col < CACol; col++)
                {
                    matrix_CA[row, col] = matrix_AA[row, col];
                }
            }

            matrix_temp1 = new int[CARow, 1];
            matrix_temp2 = new int[CARow, 1];
            matrix_temp3 = new int[CARow, 1];


            int col_left = 3;


            //create fully and correct CA matrix
            while (col_left <= CACol)
            {
                int MaxBond = 0;
                int BondPosition = 0;


                rtbx_CAMatrix.AppendText(Environment.NewLine + "-----------------------------------------------------------------");
                rtbx_CAMatrix.AppendText(Environment.NewLine + "Calculate position for A" + col_left);
                //MessageBox.Show("A" + col_left);
                //Find max bond --> Position to place A(index)
                for (int col = 0; col < col_left; col++)
                {
                    //Three values of three Bond Position
                    int Bond1 = 0;
                    int Bond2 = 0;
                    int Bond3 = 0;


                    //Case (A0, A3, A1)
                    if (col == 0)
                    {
                        rtbx_CAMatrix.AppendText(Environment.NewLine + "      cont(A0, A" + (col_left) + ", A" + CAPosition[col] + ")= ");
                        for (int row = 0; row < CARow; row++)
                        {
                            matrix_temp2[row, 0] = matrix_CA[row, (col_left - 1)]; //------>> A(index)
                            matrix_temp3[row, 0] = matrix_CA[row, col];
                            
                            Bond1 = Bond3 = 0;
                            Bond2 = Bond2 + (matrix_temp2[row, 0] * matrix_temp3[row, 0]);
                        }
                        rtbx_CAMatrix.Text = rtbx_CAMatrix.Text + (2 * Bond1 + 2 * Bond2 - 2 * Bond3);
                    }

                    //Case (A1, A3, A2) ...
                    else if (col > 0 && col < col_left - 1)
                    {
                        rtbx_CAMatrix.AppendText(Environment.NewLine + "      cont(A" + CAPosition[col-1] + ", A" + (col_left) + ", A" + CAPosition[col] + ")= ");
                        for (int row = 0; row < CARow; row++)
                        {
                            matrix_temp1[row, 0] = matrix_CA[row, (col - 1)];
                            matrix_temp2[row, 0] = matrix_CA[row, (col_left - 1)]; //------>> A(index)
                            matrix_temp3[row, 0] = matrix_CA[row, col];

                            Bond1 = Bond1 + (matrix_temp1[row, 0] * matrix_temp2[row, 0]);
                            Bond2 = Bond2 + (matrix_temp2[row, 0] * matrix_temp3[row, 0]);
                            Bond3 = Bond3 + (matrix_temp1[row, 0] * matrix_temp3[row, 0]);
                        }
                        rtbx_CAMatrix.Text = rtbx_CAMatrix.Text + (2 * Bond1 + 2 * Bond2 - 2 * Bond3);
                    }

                    //Case (A2, A3, A0)
                    else if (col == col_left - 1)
                    {
                        rtbx_CAMatrix.AppendText(Environment.NewLine + "      cont(A" + CAPosition[(col - 1)] + ", A" + (col_left) + ", A0)= ");
                        for (int row = 0; row < CARow; row++)
                        {
                            matrix_temp1[row, 0] = matrix_CA[row, (col_left - 2)];
                            matrix_temp2[row, 0] = matrix_CA[row, (col_left - 1)]; //------>> A(index)
                            Bond1 = Bond1 + matrix_temp1[row, 0] * matrix_temp2[row, 0];
                            Bond2 = Bond3 = 0;
                        }
                        rtbx_CAMatrix.Text = rtbx_CAMatrix.Text + (2 * Bond1 + 2 * Bond2 - 2 * Bond3);
                    }

                    //Find max Bond and Position of A(index)
                    if (MaxBond <= (2 * Bond1 + 2 * Bond2 - 2 * Bond3))
                    {
                        MaxBond = 2 * Bond1 + 2 * Bond2 - 2 * Bond3;
                        BondPosition = col;
                    }
                }

                rtbx_CAMatrix.AppendText(Environment.NewLine + "MaxBond is: " + MaxBond);

                //Swap Postion for A value
                //case index locate at 1st position
                rtbx_CAMatrix.AppendText(Environment.NewLine + "We choose case " + (BondPosition +1));
                if (BondPosition == 0)
                {
                    int tempPos = CAPosition[col_left - 1];

                    for (int col = col_left; col > 1; col--)
                    {
                        CAPosition[(col - 1)] = CAPosition[(col - 2)];
                    }
                    CAPosition[0] = tempPos;

                    rtbx_CAMatrix.Text = rtbx_CAMatrix.Text + "A0, A" + CAPosition[0] + ", A" + CAPosition[BondPosition + 1];
                }
                //other case
                else
                {
                    int tempPos = CAPosition[col_left - 1];

                    for (int col = col_left; col > BondPosition; col--)
                    {
                        CAPosition[(col - 1)] = CAPosition[col - 2];
                    }
                    CAPosition[BondPosition] = tempPos;
                }

                rtbx_CAMatrix.AppendText(Environment.NewLine + "New Order is: (");
                for (int i = 0; i < CACol; i++)
                {
                    rtbx_CAMatrix.Text = rtbx_CAMatrix.Text + "A" + CAPosition[i] + " ";
                }
                rtbx_CAMatrix.Text = rtbx_CAMatrix.Text + ")";


                //Swap column of CA matrix according to Position
                for (int row = 0; row < CARow; row++)
                {
                    //case index locate at 1st position
                    if (BondPosition == 0)
                    {
                        int temp = matrix_CA[row, col_left - 1]; //Store A(index) to temp

                        for (int col = col_left; col > 1; col--)
                        {
                            matrix_CA[row, (col - 1)] = matrix_CA[row, (col - 2)]; //Move A2 to A3
                        }
                        matrix_CA[row, 0] = temp;
                    }

                    //other case
                    else if (BondPosition < col_left)
                    {
                        int temp = matrix_CA[row, col_left - 1];  //Store A(index) to temp

                        for (int col = col_left; col > BondPosition; col--)
                        {
                            matrix_CA[row, (col - 1)] = matrix_CA[row, (col - 2)]; //Move A2 to A3
                        }
                        matrix_CA[row, BondPosition] = temp;
                    }
                }

                //Swap row according to Position
                for (int col = 0; col < CACol; col++)
                {
                    //case index locate at 1st position
                    if (BondPosition == 0)
                    {
                        int temp = matrix_CA[col_left - 1, col]; //Store A(index) to temp
                        for (int row = col_left; row > 1; row--)
                        {
                            matrix_CA[(row - 1), col] = matrix_CA[(row - 2), col]; //Move A2 to A3
                        }
                        matrix_CA[0, col] = temp;
                    }

                    //other case
                    else if (BondPosition < col_left)
                    {
                        int temp = matrix_CA[col_left - 1, col]; //Store A(index) to temp
                        for (int row = col_left; row > BondPosition; row--)
                        {
                            matrix_CA[(row - 1), col] = matrix_CA[(row - 2), col]; //Move A2 to A3
                        }
                        matrix_CA[BondPosition, col] = temp;
                    }
                }

                PointX = 35;
                PointY = 30;
                
                //Swap column of temp Usage according to Position
                for (int row = 0; row < Convert.ToInt32(tbx_UseRow.Text); row++)
                {
                    //case index locate at 1st position
                    if (BondPosition == 0)
                    {
                        int temp = matrix_tempUsage[row, col_left - 1]; //Store A(index) to temp

                        for (int col = col_left; col > 1; col--)
                        {
                            matrix_tempUsage[row, (col - 1)] = matrix_tempUsage[row, (col - 2)]; //Move A2 to A3
                        }
                        matrix_tempUsage[row, 0] = temp;
                    }

                    //other case
                    else if (BondPosition < col_left)
                    {
                        int temp = matrix_tempUsage[row, col_left - 1];  //Store A(index) to temp

                        for (int col = col_left; col > BondPosition; col--)
                        {
                            matrix_tempUsage[row, (col - 1)] = matrix_tempUsage[row, (col - 2)]; //Move A2 to A3
                        }
                        matrix_tempUsage[row, BondPosition] = temp;
                    }
                }

                //increas index
                col_left++;
            }

            PointX = 35;
            PointY = 30;

            //Print CA matrix
            for (int row = 0; row < CARow; row++)
            {
                PointX = 35;


                for (int col = 0; col < CACol; col++)
                {
                    Label CAattribute = new Label();
                    CAattribute.Text = matrix_CA[row, col].ToString();
                    CAattribute.Location = new Point(PointX, PointY + 5);
                    CAattribute.Size = new Size(30, 30);
                    pnl_CAMatrix.Controls.Add(CAattribute);
                    pnl_CAMatrix.Show();

                    PointX += 35;
                }
                PointY += 30;
            }

            PointX = 35;
            PointY = 30;
            //Print A(index) lable position for CA matrix
            for (int AAvalue = 0; AAvalue < CACol; AAvalue++)
            {

                PointX = 35;
                Label HorCAattribute = new Label();
                HorCAattribute.Text = ("A" + CAPosition[(AAvalue)]).ToString();
                HorCAattribute.Location = new Point(5, PointY + 5);
                HorCAattribute.Size = new Size(30, 30);
                pnl_CAMatrix.Controls.Add(HorCAattribute);
                pnl_CAMatrix.Show();
                for (int CAvalue = 0; CAvalue < CARow; CAvalue++)
                {

                    if(AAvalue == 0)
                    {
                        Label VerCAattribute = new Label();
                        VerCAattribute.Text = ("A" + CAPosition[(CAvalue)]).ToString();
                        VerCAattribute.Location = new Point(PointX + 5, 5);
                        VerCAattribute.Size = new Size(30, 30);
                        pnl_CAMatrix.Controls.Add(VerCAattribute);
                        pnl_CAMatrix.Show();
                    }

                        PointX += 35;
                }
                    PointY += 30;
            }




            //=============================Find VF=========================
            int MaxQ = 0;
            int Position = 0;

            //Print reorder Usage Maxtrix
            PointX = 35;
            PointY = 30;
            int qr = 1;

            for (int row = 0; row < Convert.ToInt32(tbx_UseRow.Text); row ++)
            {
                PointX = 35;

                Label TempQuery = new Label();
                TempQuery.Text = ("Q" + (qr++)).ToString();
                TempQuery.Location = new Point(5, PointY + 5);
                TempQuery.Size = new Size(30, 30);
                pnl_tempUseMatrix.Controls.Add(TempQuery);
                pnl_tempUseMatrix.Show();

                for (int col = 0; col < CACol; col++)
                {
                    Label CAattribute = new Label();
                    CAattribute.Text = matrix_tempUsage[row, col].ToString();
                    CAattribute.Location = new Point(PointX, PointY + 5);
                    CAattribute.Size = new Size(30, 30);
                    pnl_tempUseMatrix.Controls.Add(CAattribute);
                    pnl_tempUseMatrix.Show();

                    if(row == 0)
                    {
                        Label Hortempattribute = new Label();
                        Hortempattribute.Text = ("A" + CAPosition[col]).ToString();
                        Hortempattribute.Location = new Point(PointX + 5, 5);
                        Hortempattribute.Size = new Size(30, 30);
                        pnl_tempUseMatrix.Controls.Add(Hortempattribute);
                        pnl_tempUseMatrix.Show();
                    }
                    VerAtt++;
                    PointX += 35;
                }

                PointY += 30;
            }
            

            //Find VF
            for (int VFPosition = 0; VFPosition < CACol - 1; VFPosition++)
            {
                int CTQ = 0;
                int CBQ = 0;
                int COQ = 0;
                
                rtbx_VF.AppendText(Environment.NewLine + "==========================================");
                rtbx_VF.AppendText(Environment.NewLine + "P" + (VFPosition + 1) + ": {");

                //First Position
                if (VFPosition == 0)
                {
                    //For Printing
                    rtbx_VF.Text = rtbx_VF.Text + "A" + CAPosition[0] + "} {";
                    for (int i = 1; i < CACol; i++)
                    {
                        rtbx_VF.Text = rtbx_VF.Text + "A" + CAPosition[i] + ", ";
                    }
                    rtbx_VF.Text = rtbx_VF.Text + "}";

                    //calculate CTQ
                    rtbx_VF.AppendText(Environment.NewLine + "    TQ = {");
                    for (int row = 0; row < Convert.ToInt32(tbx_UseRow.Text); row++)
                    {
                        if (matrix_tempUsage[row, 0] == 1)
                        {
                            if (row == 0)
                                rtbx_VF.Text = rtbx_VF.Text + "Q" + (row + 1);
                            else
                                rtbx_VF.Text = rtbx_VF.Text + ", Q" + (row+1);
                            for (AFCol = 0; AFCol < Convert.ToInt32(tbx_AFCol.Text); AFCol++)
                            {
                                CTQ = CTQ + matrix_AF[row, AFCol];
                            }
                        }
                    }
                    if (CTQ == 0)
                    {
                        rtbx_VF.Text = rtbx_VF.Text + 0;
                    }
                    rtbx_VF.Text = rtbx_VF.Text + "}";

                    //calculate CBQ
                    rtbx_VF.AppendText(Environment.NewLine + "    BQ = {");
                    for (int row = 0; row < Convert.ToInt32(tbx_UseRow.Text); row++)
                    {
                        int total = 0;
                        //check if there're common queries
                        for (int tempCol = 1; tempCol < CACol; tempCol++)
                        {
                            if(matrix_tempUsage[row, tempCol] == 1)
                            {
                                total = total + 1;
                            }
                        }

                        //if there's common query in this row
                        if(total == (CACol - 1))
                        {
                            if (row == 0)
                                rtbx_VF.Text = rtbx_VF.Text + "Q" + (row + 1);
                            else
                                rtbx_VF.Text = rtbx_VF.Text + ", Q" + (row + 1);
                            for (AFCol = 0; AFCol < Convert.ToInt32(tbx_AFCol.Text); AFCol++)
                            {
                                CBQ = CBQ + matrix_AF[row, AFCol];
                            }
                        }
                    }
                    if(CBQ == 0)
                    {
                        rtbx_VF.Text = rtbx_VF.Text + 0;
                    }
                    rtbx_VF.Text = rtbx_VF.Text + "}";

                    //calculate COQ
                    rtbx_VF.AppendText(Environment.NewLine + "    OQ = {");
                    for (int row = 0; row < Convert.ToInt32(tbx_UseRow.Text); row++)
                    {
                        int total = 0;
                        //check if there're common queries
                        for (int tempCol = 0; tempCol < CACol; tempCol++)
                        {
                            if (matrix_tempUsage[row, tempCol] == 1)
                            {
                                total = total + 1;
                            }
                        }

                        //if there's common query in this row
                        if (total == CACol)
                        {
                            if (row == 0)
                                rtbx_VF.Text = rtbx_VF.Text + "Q" + (row + 1);
                            else
                                rtbx_VF.Text = rtbx_VF.Text + ", Q" + (row + 1);
                            for (AFCol = 0; AFCol < Convert.ToInt32(tbx_AFCol.Text); AFCol++)
                            {
                                COQ = COQ + matrix_AF[row, AFCol];
                            }
                        }
                    }
                    if (COQ == 0)
                    {
                        rtbx_VF.Text = rtbx_VF.Text + 0;
                    }
                    rtbx_VF.Text = rtbx_VF.Text + "}";

                    rtbx_VF.AppendText(Environment.NewLine);
                    rtbx_VF.AppendText(Environment.NewLine + "    CTQ = " + CTQ);
                    rtbx_VF.AppendText(Environment.NewLine + "    CBQ = " + CBQ);
                    rtbx_VF.AppendText(Environment.NewLine + "    COQ = " + COQ);

                    rtbx_VF.AppendText(Environment.NewLine);
                    rtbx_VF.AppendText(Environment.NewLine + "    Total = " + (CTQ*CBQ - COQ*COQ));
                    if(MaxQ <= (CTQ * CBQ - COQ * COQ))
                    {
                        MaxQ = (CTQ * CBQ - COQ * COQ);
                        Position = VFPosition;
                    }
                }
                

                //Other Postion
                else
                {
                    //For Printing
                    for(int i = 0; i < VFPosition + 1; i++)
                    {
                        if(i == 0)
                            rtbx_VF.Text = rtbx_VF.Text + "A" + CAPosition[i];
                        else
                            rtbx_VF.Text = rtbx_VF.Text + ", A" + CAPosition[i];
                    }
                    rtbx_VF.Text = rtbx_VF.Text + "} {";
                    for (int i = VFPosition + 1; i < CACol; i++)
                    {
                        if (i == VFPosition)
                            rtbx_VF.Text = rtbx_VF.Text + "A" + CAPosition[i];
                        else
                            rtbx_VF.Text = rtbx_VF.Text + ", A" + CAPosition[i];
                    }
                    rtbx_VF.Text = rtbx_VF.Text + "}";

                    //calculate CTQ
                    rtbx_VF.AppendText(Environment.NewLine + "    TQ = {");
                    for (int row = 0; row < Convert.ToInt32(tbx_UseRow.Text); row++)
                    {
                        int total = 0;
                        //check if there're common queries
                        for (int tempCol = 0; tempCol <= VFPosition; tempCol++)
                        {
                            if (matrix_tempUsage[row, tempCol] == 1)
                            {
                                total = total + 1;
                            }
                        }

                        //if there's common query in this row
                        if (total == VFPosition + 1)
                        {
                            if (row == 0)
                                rtbx_VF.Text = rtbx_VF.Text + "Q" + (row + 1);
                            else
                                rtbx_VF.Text = rtbx_VF.Text + ", Q" + (row + 1);
                            for (AFCol = 0; AFCol < Convert.ToInt32(tbx_AFCol.Text); AFCol++)
                            {
                                CTQ = CTQ + matrix_AF[row, AFCol];
                            }
                        }
                    }
                    if (CTQ == 0)
                    {
                        rtbx_VF.Text = rtbx_VF.Text + 0;
                    }
                    rtbx_VF.Text = rtbx_VF.Text + "}";

                    //calculate CBQ
                    rtbx_VF.AppendText(Environment.NewLine + "    BQ = {");
                    for (int row = 0; row < Convert.ToInt32(tbx_UseRow.Text); row++)
                    {
                        int total = 0;
                        //check if there're common queries
                        for (int tempCol = VFPosition + 1; tempCol < CACol; tempCol++)
                        {
                            if (matrix_tempUsage[row, tempCol] == 1)
                            {
                                total = total + 1;
                            }
                        }

                        //if there's common query in this row
                        if (total == (CACol - VFPosition - 1))
                        {
                            if (row == 0)
                                rtbx_VF.Text = rtbx_VF.Text + "Q" + (row + 1);
                            else
                                rtbx_VF.Text = rtbx_VF.Text + ", Q" + (row + 1);
                            for (AFCol = 0; AFCol < Convert.ToInt32(tbx_AFCol.Text); AFCol++)
                            {
                                CBQ = CBQ + matrix_AF[row, AFCol];
                            }
                        }
                    }
                    if (CBQ == 0)
                    {
                        rtbx_VF.Text = rtbx_VF.Text + 0;
                    }
                    rtbx_VF.Text = rtbx_VF.Text + "}";

                    //calculate COQ
                    rtbx_VF.AppendText(Environment.NewLine + "    OQ = {");
                    for (int row = 0; row < Convert.ToInt32(tbx_UseRow.Text); row++)
                    {
                        int total = 0;
                        //check if there're common queries
                        for (int tempCol = 0; tempCol < Convert.ToInt32(tbx_AFCol.Text); tempCol++)
                        {
                            if (matrix_tempUsage[row, tempCol] == 1)
                            {
                                total = total + 1;
                            }
                        }

                        //if there's common query in this row
                        if (total == CACol)
                        {
                            if (row == 0)
                                rtbx_VF.Text = rtbx_VF.Text + "Q" + (row + 1);
                            else
                                rtbx_VF.Text = rtbx_VF.Text + ", Q" + (row + 1);
                            for (AFCol = 0; AFCol < Convert.ToInt32(tbx_AFCol.Text); AFCol++)
                            {
                                COQ = COQ + matrix_AF[row, AFCol];
                            }
                        }
                    }
                    if (COQ == 0)
                    {
                        rtbx_VF.Text = rtbx_VF.Text + 0;
                    }
                    rtbx_VF.Text = rtbx_VF.Text + "}";

                    rtbx_VF.AppendText(Environment.NewLine);
                    rtbx_VF.AppendText(Environment.NewLine + "    CTQ = " + CTQ);
                    rtbx_VF.AppendText(Environment.NewLine + "    CBQ = " + CBQ);
                    rtbx_VF.AppendText(Environment.NewLine + "    CBQ = " + COQ);

                    rtbx_VF.AppendText(Environment.NewLine);
                    rtbx_VF.AppendText(Environment.NewLine + "    Total = " + (CTQ * CBQ - COQ * COQ));

                    if (MaxQ <= (CTQ * CBQ - COQ * COQ))
                    {
                        MaxQ = (CTQ * CBQ - COQ * COQ);
                        Position = VFPosition;
                    }
                }
            }

            rtbx_VF.AppendText(Environment.NewLine);
            rtbx_VF.AppendText(Environment.NewLine + "So we pick P" + (Position + 1) + " as our best point for Vertical Fragmentation");

        }
    }
}
