using MySqlConnector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CardShape
{
	Spade,
	Diamond,
	Heart,
	Clover,
	None
}
public enum Level
{
	Made,
	Base,
	TwoBase,
	None
}
public static class DBManager
{
    public static string UserID;
    public static string UserName;
    public static int GP;

    public static string sqlConnect = "server=localhost;uid=root;pwd=root;database=lowbadugi;charset=utf8;TlsVersion=Tlsv1.2;";
	// Start is called before the first frame update

	public static bool LoggedIn { get { return UserID != null; } }

    public static bool PasswordCheck(in string id_input, in string pwd_input, out string alarmMessage)
	{
        string id = id_input;
        string pwd = pwd_input;

        MySqlConnection conn = new MySqlConnection(sqlConnect);
		if (conn.State != System.Data.ConnectionState.Closed)
		{
            Debug.Log("connection Failed");
            alarmMessage = "DataBase에 접근할 수 없습니다.";
            return false;
		}
        conn.Open();

        string quary = "SELECT * FROM member WHERE(UserID = '" + id + "' OR UserName = '" + id + "') AND Password='" + pwd + "'";
        MySqlCommand command = new MySqlCommand(quary, conn);
        MySqlDataReader rdr = command.ExecuteReader();

		if (!rdr.HasRows)
		{
            rdr.Read();
            rdr.Close();
            conn.Close();
            alarmMessage = "아이디 또는 비밀번호가 일치하지 않습니다.";
            return false;
		}
		else
		{
            rdr.Read();
            rdr.Close();
            conn.Close();
            alarmMessage = "";
            return true;
		}
	}
    
    public static bool Login(in string id_input, in string pwd_input, out string alarmMessage)
	{
		string id = id_input;
		string password = pwd_input;
		
		MySqlConnection conn = new MySqlConnection(sqlConnect);
		if (conn.State != System.Data.ConnectionState.Closed)
		{
			Debug.Log("connetion Failed");
			alarmMessage = "DataBase에 접근할 수 없습니다.";
			return false;
		}
		conn.Open();

		string quary = "SELECT * FROM member  WHERE(UserID = '" + id + "' OR UserName = '" + id + "') AND Password='" + password + "'";
		MySqlCommand command = new MySqlCommand(quary, conn);
		MySqlDataReader rdr = command.ExecuteReader();

		if (!rdr.HasRows)
		{
			rdr.Read();
			rdr.Close();
			conn.Close();
			alarmMessage = "아이디 또는 비밀번호가 일치하지 않습니다.";
			return false;
		}
		else
		{
			rdr.Read();
			string tempid = rdr["UserID"].ToString();
			string tempnickname = rdr["UserName"].ToString();
			int tempgp = int.Parse(rdr["GP"].ToString());

			//if (int.Parse(rdr["out"].ToString()) == 1)
			//{
			//	alarmMessage = "탈퇴 신청 중인 아이디입니다.";
			//	rdr.Close();
			//	conn.Close();
			//	return false;
			//}
			//else if (int.Parse(rdr["out"].ToString()) == 2)
			//{
			//	alarmMessage = "회원 탈퇴된 아이디입니다.";
			//	rdr.Close();
			//	conn.Close();
			//	return false;
			//}
			//else if (int.Parse(rdr["out"].ToString()) == 3)
			//{
			//	alarmMessage = "정지 된 아이디입니다.";
			//	rdr.Close();
			//	conn.Close();
			//	return false;
			//}
			rdr.Close();

			UserID = tempid;
			UserName = tempnickname;
			GP = tempgp;

			conn.Close();

			alarmMessage = "";
			return true;
		}

	}
	public static bool LogOut()
	{
		if (!LoggedIn)
		{
			return true;
		}

		MySqlConnection conn = new MySqlConnection(sqlConnect);
		if (conn.State != System.Data.ConnectionState.Closed)
		{
			Debug.Log("connetion Failed");
			return false;
		}
		conn.Open();

		conn.Close();

		UserID = null;
		UserName = null;
		GP = 0;
		return true;
	}
}
