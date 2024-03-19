using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace Ticari_Otamasyon
{
	class sqlbaglantisi
	{
		public SqlConnection baglanti()
		{
			SqlConnection baglan = new SqlConnection(@"Data Source=LAPTOP-FA6RBVRG;Initial Catalog=DboTicariOtomasyon;Integrated Security=True");
			baglan.Open();
			return baglan;
		}
	}
}
