using System.Collections;
using System.Reflection.Metadata.Ecma335;
using System.Windows.Forms.VisualStyles;
using static System.Net.Mime.MediaTypeNames;

namespace Caculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private bool IsAnswer = false;


        private void Form1_Load(object sender, EventArgs e)
        {
            this.MaximizedBounds = new Rectangle(this.Location, new Size(this.Size.Width * 2, this.Size.Height * 2));
            //this.ResumeLayout(false);

        }

        private void Button_1_Click(object sender, EventArgs e)
        {
            SetEmptyStringWhenException();
            SetEmptyStringWhenAnswer();
            this.textBox1.Text += this.Button_1.Text;
        }

        private void Button_2_Click(object sender, EventArgs e)
        {
            SetEmptyStringWhenException();
            SetEmptyStringWhenAnswer();
            this.textBox1.Text += this.Button_2.Text;

        }

        private void Button_3_Click(object sender, EventArgs e)
        {
            SetEmptyStringWhenException();
            SetEmptyStringWhenAnswer();
            this.textBox1.Text += this.Button_3.Text;

        }

        private void Button_4_Click(object sender, EventArgs e)
        {
            SetEmptyStringWhenException();
            SetEmptyStringWhenAnswer();
            this.textBox1.Text += this.Button_4.Text;

        }

        private void Button_5_Click(object sender, EventArgs e)
        {
            SetEmptyStringWhenException();
            SetEmptyStringWhenAnswer();
            this.textBox1.Text += this.Button_5.Text;

        }

        private void Button_6_Click(object sender, EventArgs e)
        {
            SetEmptyStringWhenException();
            SetEmptyStringWhenAnswer();
            this.textBox1.Text += this.Button_6.Text;

        }

        private void Button_7_Click(object sender, EventArgs e)
        {
            SetEmptyStringWhenException();
            SetEmptyStringWhenAnswer();
            this.textBox1.Text += this.Button_7.Text;

        }

        private void Button_8_Click(object sender, EventArgs e)
        {
            SetEmptyStringWhenException();
            SetEmptyStringWhenAnswer();
            this.textBox1.Text += this.Button_8.Text;

        }

        private void Button_9_Click(object sender, EventArgs e)
        {
            SetEmptyStringWhenException();
            SetEmptyStringWhenAnswer();
            this.textBox1.Text += this.Button_9.Text;

        }

        private void Button_dot_Click(object sender, EventArgs e)
        {
            SetEmptyStringWhenException();
            SetEmptyStringWhenAnswer();
            SetDot();
        }

        private void Button_0_Click(object sender, EventArgs e)
        {
            SetEmptyStringWhenException();
            SetEmptyStringWhenAnswer();
            this.textBox1.Text += this.Button_0.Text;
        }

        private void Button_add_Click(object sender, EventArgs e)
        {
            SetEmptyStringWhenException();

            if (!string.IsNullOrEmpty(this.textBox1.Text))
                if (this.textBox1.Text.Last() == '+' || this.textBox1.Text.Last() == '-'
                    || this.textBox1.Text.Last() == '*' || this.textBox1.Text.Last() == '/')
                {
                    this.textBox1.Text = this.textBox1.Text.Replace(this.textBox1.Text.Last(), '+');
                    return;
                }
            this.textBox1.Text += "+";
        }

        private void Button_sub_Click(object sender, EventArgs e)
        {
            SetEmptyStringWhenException();

            if (!string.IsNullOrEmpty(this.textBox1.Text))
                if (this.textBox1.Text.Last() == '+' || this.textBox1.Text.Last() == '-'
                    || this.textBox1.Text.Last() == '*' || this.textBox1.Text.Last() == '/')
                {
                    this.textBox1.Text = this.textBox1.Text.Replace(this.textBox1.Text.Last(), '-');
                    return;
                }
            this.textBox1.Text += "-";
        }

        private void Button_mul_Click(object sender, EventArgs e)
        {
            SetEmptyStringWhenException();

            if (!string.IsNullOrEmpty(this.textBox1.Text))
                if (this.textBox1.Text.Last() == '+' || this.textBox1.Text.Last() == '-'
                    || this.textBox1.Text.Last() == '*' || this.textBox1.Text.Last() == '/')
                {
                    this.textBox1.Text = this.textBox1.Text.Replace(this.textBox1.Text.Last(), '*');
                    return;
                }
            this.textBox1.Text += "*";
        }

        private void Button_div_Click(object sender, EventArgs e)
        {
            SetEmptyStringWhenException();

            if (!string.IsNullOrEmpty(this.textBox1.Text))
                if (this.textBox1.Text.Last() == '+' || this.textBox1.Text.Last() == '-'
                    || this.textBox1.Text.Last() == '*' || this.textBox1.Text.Last() == '/')
                {
                    this.textBox1.Text = this.textBox1.Text.Replace(this.textBox1.Text.Last(), '/');
                    return;
                }
            this.textBox1.Text += "/";
        }

        private void Button_ac_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = null;
        }
        private void Button_eq_Click(object sender, EventArgs e)
        {
            string text_mid = new string(this.textBox1.Text);
            List<object> text_hou = new List<object>();
            Dictionary<char, int> opStatus = new Dictionary<char, int> { { '*', 1 }, { '/', 1 }, { '+', 0 }, { '-', 0 } };

            double result; //表达式最终结果

            // 判断非法
            if (JudgeIllegalCondition(text_mid))
            {
                return;
            }

            // 补全0
            AddZero(ref text_mid);

            // 转成后缀表达式，如果在转化表达式的过程中发现了非法运算数和非法除法
            text_hou = ChangeHouZhui(text_mid, opStatus);

            //如果存在非法运算数和非法除法
            if (text_hou is null) return;

            // 计算后缀表达式
            result = CaculateHouZhui(text_hou, opStatus);

            // 显示
            this.textBox1.Text = result.ToString();

            //设置为答案
            SetIsAnswer(true);


        }


        /// <summary>
        /// 计算后缀表达式。
        /// </summary>
        /// <param name="text_hou"></param>
        /// <param name="opStatus"></param>
        /// <returns></returns>
        private double CaculateHouZhui(List<object> text_hou, Dictionary<char, int> opStatus)
        {
            double result = -1;

            // 当只有一个数字时，直接输出该数字
            if (text_hou.Count == 1)
            {
                result = (double)text_hou[0];
                return result;
            }

            //创建两个栈 
            Stack<double> stack_num = new Stack<double>();
            Stack<char> stack_op = new Stack<char>();

            foreach (object item in text_hou)
            {
                if (item is double)
                {
                    stack_num.Push((double)item);
                }
                else //如果是运算符，就出栈两个操作数进行运算。
                {
                    char op = (char)item;
                    double num1 = stack_num.Pop();
                    double num2 = stack_num.Pop();
                    double temp;

                    switch (op)
                    {
                        case '+': temp = num2 + num1; break;
                        case '-': temp = num2 - num1; break;
                        case '*': temp = num2 * num1; break;
                        case '/': temp = num2 / num1; break;
                        default: temp = 0; break;
                    }
                    if (text_hou.Last() != item)
                    {
                        stack_num.Push(temp);
                    }
                    else
                    {
                        result = temp;
                    }

                }
            }
            return result;
        }

        /// <summary>
        /// 生成后缀表达式。
        /// </summary>
        /// <param name="text_mid"></param>
        /// <param name="opStatus"></param>
        /// <returns></returns>
        private List<object> ChangeHouZhui(string text_mid, Dictionary<char, int> opStatus)
        {
            List<object> text_hou = new List<object>();
            //创建两个栈 
            Stack<double> stack_num = new Stack<double>();
            Stack<char> stack_op = new Stack<char>();

            // 是否有运算符
            bool flag = false;


            int index = 0;
            int pre = 0, p = 0; //上一个符号的下一位、当前符号
            // 这事在找到运算符时，进行的逻辑
            while (index < text_mid.Length)
            {
                if (text_mid[index] == '+' || text_mid[index] == '-'
                    || text_mid[index] == '*' || text_mid[index] == '/')
                {
                    p = index;
                    string str = text_mid.Substring(pre, p - pre);
                    double num;
                    //判断 操作符前是否是 非数字
                    if (JudgeIsNotNumber(str, out num))
                    {
                        this.textBox1.Text = "输入非数字";
                        return null;
                    }
                    text_hou.Add(num);

                    //判断前一个是否发生了非法除法。
                    if (pre != 0 && text_mid[pre - 1] == '/' && num == 0)
                    {
                        this.textBox1.Text = "非法除法";
                        return null;
                    }

                    // 栈中优先级大于等于当前操作符的先出栈
                    while (stack_op.Count != 0 && opStatus[stack_op.Peek()] >= opStatus[text_mid[index]])
                    {
                        char temp = stack_op.Pop();
                        text_hou.Add(temp);
                    }
                    stack_op.Push(text_mid[index]);
                    pre = p + 1;

                    //判断是否是最后一个运算符
                    if (!text_mid.Substring(pre, text_mid.Length - pre).Any<char>(c => c == '+' || c == '-'
                    || c == '*' || c == '/'))
                    {
                        string s = text_mid.Substring(pre, text_mid.Length - pre);
                        double num1;
                        //最后一个运算符，判断后一个是否为非数字
                        if (JudgeIsNotNumber(s, out num1))
                        {
                            this.textBox1.Text = "输入非数字";
                            return null;
                        }

                        // 最后一个运算符，判断后一个是否发生了非法除法。
                        if (text_mid[p] == '/' && num1 == 0)
                        {
                            this.textBox1.Text = "非法除法";
                            return null;
                        }

                        text_hou.Add(num1);
                        flag = true;
                        break;
                    }

                    // 表达式中存在运算符

                }
                ++index;
            }

            // 将运算符 出栈
            while (stack_op.Count != 0)
            {
                char temp = (char)stack_op.Pop();
                text_hou.Add(temp);
            }

            // 如果没有操作符
            if (flag is false)
            {
                double num;
                if (JudgeIsNotNumber(text_mid, out num))
                {
                    this.textBox1.Text = "非数字";
                    return null;
                }
                else
                {
                    text_hou.Add(double.Parse(text_mid));
                }
            }

            return text_hou;
        }
        /// <summary>
        /// 判断是否是数字。
        /// </summary>
        /// <param name="str"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        private bool JudgeIsNotNumber(string str, out double num)
        {
            bool flag = double.TryParse(str, out num);
            return !flag;
        }

        /// <summary>!
        /// 当存在上次的错误提示 或者 框中是上次的答案，重新输入需要清空textBox.
        /// </summary>
        private void SetEmptyStringWhenException()
        {
            if (textBox1.Text.Any<char>(c => (c >= '\u4E00' && c <= '\u9FA5')
            || (c >= '\u3400' && c <= '\u4DBF')))
            {
                this.textBox1.Text = "";
            }
            this.IsAnswer = false;
        }
        /// <summary>
        ///框中是上次的答案，重新输入需要清空textBox.
        /// </summary>
        private void SetEmptyStringWhenAnswer()
        {
            if (this.IsAnswer == true)
            {
                this.textBox1.Text = "";
                this.IsAnswer = false;
            }
        }
        /// <summary>
        /// 向前探寻，直到边界或者一个运算符，如果存在dot，则不应该输入，反之输入。
        /// </summary>
        private void SetDot()
        {
            bool flag = false;
            if (!string.IsNullOrEmpty(this.textBox1.Text))
                for (int index = this.textBox1.Text.Length - 1; index >= 0 && this.textBox1.Text[index] != '+' && this.textBox1.Text[index] != '-'
                    && this.textBox1.Text[index] != '*' && this.textBox1.Text[index] != '/'; index--)
                {
                    if (this.textBox1.Text[index] == '.')
                    {
                        flag = true;
                    }
                }
            if (!flag)
            {
                this.textBox1.Text += ".";
            }
        }
        /// <summary>
        /// 如果首位为正负号，则在首尾补一个0.
        /// </summary>
        /// <param name="text_mid"></param>
        private void AddZero(ref string text)
        {
            if (this.textBox1.Text[0] == '+' || this.textBox1.Text[0] == '-')
            {
                text = "0" + text;
            }
        }

        /// <summary>
        /// 判断非法。
        /// </summary>
        /// <param name="text_mid"></param>
        /// <returns></returns>
        private bool JudgeIllegalCondition(string text_mid)
        {
            bool flag = false;

            //情况一 当只有一个运算符时
            if (this.textBox1.Text == "+" || this.textBox1.Text == "-"
                || this.textBox1.Text == "*" || this.textBox1.Text == "/")
            {
                this.textBox1.Text = "单运算符错误";
                flag = true;
                goto distinct;
            }
            //情况二 当乘法和除法位于首位时
            if (this.textBox1.Text[0] == '*' || this.textBox1.Text[0] == '/')
            {
                this.textBox1.Text = "乘法和除法位于首尾";
                flag = true;
                goto distinct;
            }
            //情况三 当表达式中只有运算符时 或者当表达式中只有.和运算符时
            if (!textBox1.Text.Any<char>(c => char.IsNumber(c)))
            {
                this.textBox1.Text = "只存在运算符";
                flag = true;
                goto distinct;
            }

        ////情况四 当存在 除零情况 时，直接打印错误 改
        //for (int i = 0; i < text_mid.Length - 1; i++)
        //{
        //    // 判断 除法 0
        //    if (text_mid[i] == '/' && text_mid[i + 1] == '0')
        //    {
        //        this.textBox1.Text = "除法错误";
        //        flag = true;
        //        break;
        //    }
        //}

        distinct: return flag;
        }

        /// <summary>
        /// 设置答案标签。
        /// </summary>
        private void SetIsAnswer(bool flag)
        {
            if (flag)
                IsAnswer = true;
            else
                IsAnswer = false;
        }
    }

}
