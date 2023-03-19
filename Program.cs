namespace Modul_7
{
    namespace Delivery_System
    {

        // Класс, описывающий заказ
        public class Order
        {
            public int Number { get; set; }
            public string CustomerName { get; set; }
            public string ShippingAddress { get; set; }
            public List<OrderItem> OrderItems { get; set; }

            // Метод, расчитывающий общую стоимость заказа
            public decimal CalculateTotalCost()
            {
                decimal total = 0;
                foreach (OrderItem item in OrderItems)
                {
                    total += item.CalculateCost();
                }
                return total;
            }
        }


        // Класс, описывающий товар в заказе
        public class OrderItem
        {
            public string ProductName { get; set; }
            public int Quantity { get; set; }
            public decimal PricePerUnit { get; set; }

            // Метод, расчитывающий стоимость товара
            public decimal CalculateCost()
            {
                return Quantity * PricePerUnit;
            }
        }


        // Класс, описывающий способ доставки
        public abstract class DeliveryMethod
        {
            public string Name { get; set; }
            public decimal Price { get; set; }

            // Абстрактный метод, расчитывающий стоимость доставки
            public abstract decimal CalculateDeliveryCost(Order order);
        }


        // Класс, описывающий доставку курьером
        public class CourierDelivery : DeliveryMethod
        {
            public CourierDelivery()
            {
                Name = "Курьером";
                Price = 100;
            }

            // Реализация абстрактного метода
            public override decimal CalculateDeliveryCost(Order order)
            {
                return Price + order.CalculateTotalCost() * 0.05m;
            }
        }


        // Класс, описывающий доставку почтой
        public class MailDelivery : DeliveryMethod
        {
            public MailDelivery()
            {
                Name = "Почтой";
                Price = 50;
            }

            public override decimal CalculateDeliveryCost(Order order)
            {
                return Price + order.CalculateTotalCost() * 0.1m;
            }
        }



        // Класс, описывающий способ оплаты
        public interface IPaymentMethod
        {
            // Метод, проводящий оплату заказа
            void PayForOrder(Order order);
        }

        // Класс, описывающий оплату наличными
        public class CashPayment : IPaymentMethod
        {
            // Реализация метода интерфейса
            public void PayForOrder(Order order)
            {
                Console.WriteLine("Оплата заказа наличными.");
            }
        }

        // Класс, описывающий оплату картой
        public class CardPayment : IPaymentMethod
        {
            public void PayForOrder(Order order)
            {
                Console.WriteLine("Оплата заказа картой.");
            }
        }


        // Класс, описывающий покупателя
        public class Customer
        {
            // Поля
            public string Name { get; set; }

            // Свойство, возвращающее доступные способы оплаты
            public List<IPaymentMethod> AvailablePaymentMethods { get; set; }

            // Метод, размещающий заказ
            public void PlaceOrder(Order order, DeliveryMethod deliveryMethod, IPaymentMethod paymentMethod)
            {
                // Рассчитываем стоимость доставки
                decimal deliveryCost = deliveryMethod.CalculateDeliveryCost(order);

                // Выводим информацию о заказе
                Console.WriteLine("Сумма заказа: " + order.CalculateTotalCost() + " руб.");
                Console.WriteLine("Стоимость доставки: " + deliveryCost + " руб.");
                Console.WriteLine("Общая сумма заказа с доставкой: " + (order.CalculateTotalCost() + deliveryCost) + " руб.");

                // Проводим оплату
                paymentMethod.PayForOrder(order);

                // Завершаем заказ
                Console.WriteLine("Заказ оформлен и отправлен по адресу: " + order.ShippingAddress);
            }
        }
    }
}