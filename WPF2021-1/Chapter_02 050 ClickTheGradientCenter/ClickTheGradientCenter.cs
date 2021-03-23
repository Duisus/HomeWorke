using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfApplication1
{
    public class ClickTheGradientCenter : Window
    {
        private RadialGradientBrush brush;

        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new ClickTheGradientCenter());
        }

        public ClickTheGradientCenter()
        {
            Title = "Click the Gradient Center";  // Задаем заголовок окна
            brush = new RadialGradientBrush(Colors.White, Colors.DarkGreen);

            brush.RadiusX = brush.RadiusY = 0.1;  // Задаем полуоси эллипса радиального градиента

            // Настройка, из-за которой градиент повторяется в исходном порядке, пока не будет заполнено все пространство
            brush.SpreadMethod = GradientSpreadMethod.Repeat;

            Background = brush;  // задаем кисть, описывающую фон окна (в нашем случае это радиальный градиент)
        }

        protected override void OnMouseDown(MouseButtonEventArgs args)
        {
            // width - это ширина внутренней области окна
            // (та область, которая в нашем случае закрашивается градиентом)
            var width = ActualWidth  // отображаемая ширина окна (т.е. с учетом вертикальных границ рамки по бокам)
                        - 2 * SystemParameters.ResizeFrameVerticalBorderWidth;  // вычитаем толщину 2-х вертикальных границ (левой и правой)

            // height - это высота внутренней области окна
            var height = ActualHeight  // отображаемая высота окна (т.е. с учетом горизонтальных границ рамки)
                         - 2 * SystemParameters.ResizeFrameHorizontalBorderHeight  // вычитаем толщину 2-х горизонтальных границ (верхней и нижней)
                         - SystemParameters.CaptionHeight;  // вычитаем высоту заголовка (шапки) окна

            // Получаем положение курсора в окне, в абсолютных величинах
            var ptMouse = args.GetPosition(this);

            // Переводим в относительные величины, чтобы в дальнейшем изменять параметры градиента
            ptMouse.X /= width;
            ptMouse.Y /= height;

            // Проверяем какая кнопка мыши нажата, и выполняем соответствующие действия
            if (args.ChangedButton == MouseButton.Left)
            {
                brush.Center = ptMouse;  // устанавливаем центр наружного круга радиального градиента.
                brush.GradientOrigin = ptMouse;  // задаем положение двумерной фокальной точки, которая определяет начало градиента
            }
            else if (args.ChangedButton == MouseButton.Right)
                brush.GradientOrigin = ptMouse;
        }
    }
}
