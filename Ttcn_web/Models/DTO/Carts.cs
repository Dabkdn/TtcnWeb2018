using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ttcn_web.Models;

namespace Ttcn_web.Models.DTO
{
    public class Carts
    {
        public string _customerName = string.Empty;
        public string _deliveryAddress = string.Empty;
        public DateTime? _deliveryDate = DateTime.Now;
        public decimal _subTotalAmount = 0;
        public decimal _feeShipping = 0;
        public decimal _totalAmount = 0;
        public List<ARSaleOrderItem> _saleOrderItemList = new List<ARSaleOrderItem>();

        public string CustomerName
        {
            get { return _customerName; }
            set
            {
                if (value != this._customerName)
                {
                    _customerName = value;
                }
            }
        }
        public string DeliveryAddress
        {
            get { return _deliveryAddress; }
            set
            {
                if (value != this._deliveryAddress)
                {
                    _deliveryAddress = value;
                }
            }
        }
        public DateTime? DeliveryDate
        {
            get { return _deliveryDate; }
            set
            {
                if (value != this._deliveryDate)
                {
                    _deliveryDate = value;
                }
            }
        }
        public decimal SubTotalAmount
        {
            get { return _subTotalAmount; }
            set
            {
                if (value != this._subTotalAmount)
                {
                    _subTotalAmount = value;
                }
            }
        }
        public decimal FeeShipping
        {
            get { return _feeShipping; }
            set
            {
                if (value != this._feeShipping)
                {
                    _feeShipping = value;
                }
            }
        }
        public decimal TotalAmount
        {
            get { return _totalAmount; }
            set
            {
                if (value != this._totalAmount)
                {
                    _totalAmount = value;
                }
            }
        }
        public List<ARSaleOrderItem> SaleOrderItemList
        {
            get { return _saleOrderItemList; }
            set
            {
                if (value != this._saleOrderItemList)
                {
                    _saleOrderItemList = value;
                }
            }
        }
    }
}