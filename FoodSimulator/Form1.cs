using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FoodSimulator
{
    public partial class Form1 : Form
    {
        private readonly Queue<int> _primaryServer = new Queue<int>();
        private readonly Queue<Customer> _customersQueue = new Queue<Customer>();
        private readonly Queue<Customer> _primaryServerQueue = new Queue<Customer>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitilizeData();


            var currentTime = 0;

            var totalCustomerServed = 0;
            var primaryserverWaitTime = 0;
            var primaryServerWaitedCustomers = 0;
            double averageTimeinPrimaryQueue = 0;
            int averageLengthofPrimaryQueue = 0;
            int maximumLengthofPrimaryQueue = 0;


            while (true)
            {
                currentTime++;

                while (true)
                {
                    if (_primaryServerQueue.Count > 0)
                    {
                        if (_primaryServerQueue.First().PrimaryQueueTime <= currentTime)
                        {
                            _primaryServerQueue.Dequeue();
                            _primaryServer.Enqueue(1);
                            continue;
                        }
                    }
                    
                    if (currentTime == _customersQueue.First().InTime)
                    {
                        var currentCustomer = _customersQueue.Dequeue();

                        if (_primaryServer.Count > 0)
                        {
                            averageTimeinPrimaryQueue += currentCustomer.PrimaryQueueTime;
                            currentCustomer.PrimaryQueueTime += currentTime -1;
                            _primaryServerQueue.Enqueue(currentCustomer);
                            totalCustomerServed++;
                            _primaryServer.Dequeue();
                            continue;
                        }
                        primaryserverWaitTime++;
                        primaryServerWaitedCustomers++;
                        averageLengthofPrimaryQueue++;
                        maximumLengthofPrimaryQueue++;
                    }
                    else
                    {
                        if (_customersQueue.First().InTime == 0 && _primaryServerQueue.Count == 0)
                        {
                            _customersQueue.Dequeue();
                        }
                    }
                    break;
                }
                if (_customersQueue.Count == 0)
                {
                    break;
                }

            }

            averageTimeinPrimaryQueue = averageTimeinPrimaryQueue / totalCustomerServed;

            MessageBox.Show("Number of people served: " + totalCustomerServed);
            MessageBox.Show("Primary Server Wait Time: " + primaryserverWaitTime);
            MessageBox.Show("Primary Server Waited Customers: " + primaryServerWaitedCustomers);
            MessageBox.Show("Average Time in Primary Queue: " + averageTimeinPrimaryQueue);
            MessageBox.Show("Average Length of Primary Queue: " + averageLengthofPrimaryQueue/totalCustomerServed);
            MessageBox.Show("Maximum Length of Primary Queue: " + averageLengthofPrimaryQueue / totalCustomerServed);
        }

        private void InitilizeData()
        {
            var customer1 = new Customer
            {
                InTime = 1,
                PrimaryQueueTime = 2,
                SecondaryQueueTime = 3
            };

            var customer2 = new Customer
            {
                InTime = 3,
                PrimaryQueueTime = 3,
                SecondaryQueueTime = 5
            };

            var customer3 = new Customer
            {
                InTime = 3,
                PrimaryQueueTime = 2,
                SecondaryQueueTime = 2
            };
            var customer4 = new Customer
            {
                InTime = 4,
                PrimaryQueueTime = 3,
                SecondaryQueueTime = 2
            };
            var customer5 = new Customer
            {
                InTime = 5,
                PrimaryQueueTime = 2,
                SecondaryQueueTime = 4
            };
            var customer6 = new Customer
            {
                InTime = 0,
                PrimaryQueueTime = 0,
                SecondaryQueueTime = 0
            };

            _customersQueue.Clear();
            _customersQueue.Enqueue(customer1);
            _customersQueue.Enqueue(customer2);
            _customersQueue.Enqueue(customer3);
            _customersQueue.Enqueue(customer4);
            _customersQueue.Enqueue(customer5);
            _customersQueue.Enqueue(customer6);

            _primaryServer.Clear();
            _primaryServer.Enqueue(1);
            _primaryServer.Enqueue(2);
            _primaryServer.Enqueue(3);

        }
    }
    public class Customer
    {
        public int InTime;
        public int PrimaryQueueTime;
        public int SecondaryQueueTime;
    }

}
