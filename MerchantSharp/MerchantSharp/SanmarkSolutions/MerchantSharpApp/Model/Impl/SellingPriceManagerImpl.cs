using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Common;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Dao;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Entities;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.Utility.UIComponents;
using MerchantSharp.SanmarkSolutions.MerchantSharpApp.View.ShopManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MerchantSharp.SanmarkSolutions.MerchantSharpApp.Model.Impl {
	class SellingPriceManagerImpl {

		private IDao dao = null;
		private AddSellingPrice addSellingPrice;

		public SellingPriceManagerImpl() {
			dao = SellingPriceDao.getInstance();
		}

		public SellingPriceManagerImpl(View.ShopManagement.AddSellingPrice addSellingPrice) {
			// TODO: Complete member initialization
			this.addSellingPrice = addSellingPrice;
			dao = SellingPriceDao.getInstance();
		}

		public int add(Entity entity) {
			return dao.add(entity);
		}

		public bool del(Entity entity) {
			return dao.del(entity);
		}

		public List<SellingPrice> get(Entity entity) {
			return dao.get(entity).Cast<SellingPrice>().ToList();
		}

		public int upd(Entity entity) {
			return dao.upd(entity);
		}

		public List<SellingPrice> getSellingPriceByItemAndMode(int itemId, String mode) {
			List<SellingPrice> list = null;
			try {
				SellingPrice sellingPrice = new SellingPrice();
				sellingPrice.ItemId = itemId;
				sellingPrice.Mode = mode;
				list = get(sellingPrice);
			} catch(Exception) {
			}
			return list;
		}

		public bool isDublicatePrice(int itemId, String unit, double price) {
			bool b = false;
			try {
				SellingPrice sellingPrice = new SellingPrice();
				sellingPrice.ItemId = itemId;
				sellingPrice.Mode = unit;
				//sellingPrice.Price = price;
				List<SellingPrice> list = get(sellingPrice);
				foreach(SellingPrice p in list) {
					if(price == p.Price) {
						b = true;
						return b;
					}
				}
			} catch(Exception) {
			}
			return b;
		}

		public bool deleteSellingPriceById(int id) {
			bool b = false;
			try {
				SellingPrice sellingPrice = new SellingPrice();
				sellingPrice.Id = id;
				b = del(sellingPrice);
			} catch(Exception) {
			}
			return b;
		}


		/// ********************************************************************************************************* ///

		internal bool addPriceFromAddSellingPrice() {
			bool b = false;
			try {
				if(addSellingPrice.textBox_price.DoubleValue == 0) {
					ShowMessage.error(Common.Messages.Error.Error006);
				} else if(isDublicatePrice(addSellingPrice.ItemId, addSellingPrice.Mode, addSellingPrice.textBox_price.DoubleValue)) {
					ShowMessage.error(Common.Messages.Error.Error007);
				} else {
					SellingPrice sellingPrice = new SellingPrice();
					sellingPrice.ItemId = addSellingPrice.ItemId;
					sellingPrice.Mode = addSellingPrice.Mode;
					sellingPrice.Price = addSellingPrice.textBox_price.DoubleValue;
					CommonMethods.setCDMDForAdd(sellingPrice);
					int id = add(sellingPrice);
					UIComboBox.sellingPriceForItemAndMode(addSellingPrice.ComboBox, addSellingPrice.ItemId, addSellingPrice.Mode, addSellingPrice);
					addSellingPrice.ComboBox.SelectedValue = id;					
					b = true;
				}
			} catch(Exception) {
			}
			return b;
		}
	}
}
