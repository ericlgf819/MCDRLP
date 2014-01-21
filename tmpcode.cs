string inputString = this.txtVendorNo.Text.Trim();
DataSet ds = null;
FrmWait frm = new FrmWait(() =>
		{
		base.ExecuteAction(() =>
			{
			ds = this.vendorBLL.SelectVendorByNoOrName(inputString);
			}, "读取业主信息出错", "读取业主信息出错");

		}, base.GetMessage("WaitSelect"));
frm.ShowDialog();

if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
{
	//将模糊查询的业主信息绑定到业主下拉框
	DataTable dt = ds.Tables[0];
	dt.Columns.Add("VendorNoName", typeof(string));
	foreach (DataRow dr in dt.Rows)
	{
		dr["VendorNoName"] = dr["VendorNo"] + ":" + dr["VendorName"];
	}
	this.cmbVendorName.DataSource = dt;
	this.cmbVendorName.ValueMember = "VendorNo";
	this.cmbVendorName.DisplayMember = "VendorNoName";
