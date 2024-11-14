using System.Collections;
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
            SetDotNoChange();
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

            // 转成后缀表达式
            text_hou = ChangeHouZhui(text_mid, opStatus);

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

            int index = 0;
            int pre = 0, p = 0; //上一个符号的下一位、当前符号
            while (index < text_mid.Length)
            {
                if (text_mid[index] == '+' || text_mid[index] == '-'
                    || text_mid[index] == '*' || text_mid[index] == '/')
                {
                    p = index;
                    string str = text_mid.Substring(pre, p - pre);
                    text_hou.Add(double.Parse(str));

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
                        text_hou.Add(double.Parse(s));
                        break;
                    }
                }
                ++index;
            }
            // 将运算符 出栈
            while (stack_op.Count != 0)
            {
                char temp = (char)stack_op.Pop();
                text_hou.Add(temp);
            }

            return text_hou;
        }

        /// <summary>
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
        /// 输入dot.
        /// </summary>
        private void SetDotNoChange()
        {
            if (!string.IsNullOrEmpty(this.textBox1.Text))
                if (textBox1.Text.Last() == '.')
                {
                    return;
                }
            this.textBox1.Text += ".";
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

            //情况四 当存在 除零情况 时，直接打印错误
            for (int i = 0; i < text_mid.Length - 1; i++)
            {
                // 判断 除法 0
                if (text_mid[i] == '/' && text_mid[i + 1] == '0')
                {
                    this.textBox1.Text = "除法错误";
                    flag = true;
                    break;
                }
            }

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
