using System;
using ModbusTCP;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Modbus
{
	public class Controller : MonoBehaviour
	{
		[SerializeField]
		private TMP_InputField txtIP;
		[SerializeField]
		private TMP_InputField txtUnit;
		[SerializeField]
		private TMP_InputField txtSize;
		[SerializeField]
		private TMP_InputField txtStartAdress;
		[SerializeField]
		private Toggle radWord;
		[SerializeField]
		private TextMeshProUGUI outputTextPrefab;
		[SerializeField]
		private Transform outputParentTransform;
		[SerializeField]
		private TextMeshProUGUI consoleText;

		private ModbusTCP.Master MBmaster;
		private List<float> data;
		private bool showOutput = false;

        private void Awake()
        {
			txtIP.text = "127.0.0.1";
            txtUnit.text = "0";
            txtSize.text = "10";
            txtStartAdress.text = "0";
        }

        private void Update()
        {
            if(showOutput)
            {
				foreach(Transform child in outputParentTransform)
                {
					Destroy(child.gameObject);
                }

				foreach (float output in data)
				{
					Debug.Log(output.ToString());
					TextMeshProUGUI outputValue = Instantiate(outputTextPrefab, outputParentTransform);
					outputValue.text = output.ToString();
				}

				showOutput = false;
			}
        }

        public void btnConnect_Click()
		{
			consoleText.text = "Connecting...";
			try
			{
				MBmaster = new Master(txtIP.text, 502, true);
				MBmaster.OnResponseData += new ModbusTCP.Master.ResponseData(MBmaster_OnResponseData);
				MBmaster.OnException += new ModbusTCP.Master.ExceptionData(MBmaster_OnException);
				consoleText.text = "Connected...";
			}
			catch (SystemException error)
			{
				consoleText.text = error.Message;
			}
		}

		public void btnReadHoldReg_Click()
		{
			ushort ID = 3;
			byte unit = Convert.ToByte(txtUnit.text);
			ushort StartAddress = ReadStartAdr();
			UInt16 Length = Convert.ToUInt16(txtSize.text);

			MBmaster.ReadHoldingRegister(ID, unit, StartAddress, Length);
		}

        private void MBmaster_OnResponseData(ushort ID, byte unit, byte function, byte[] values)
		{
			data = new();

			for (int i = 1; i < values.Length; i += 2)
			{
                float temp = (float)values[i];
				data.Add(temp);
            }

			showOutput = true;
        }

        public static float[] ToFloatArray(Byte[] array)
        {
            float[] floats = new float[array.Length / 4];
            for (int i = 0; i < floats.Length; i++)
                floats[i] = BitConverter.ToSingle(array, i * 4);
            return (floats);
        }

        private void MBmaster_OnException(ushort id, byte unit, byte function, byte exception)
		{
			string exc = "Modbus says error: ";
			switch (exception)
			{
				case Master.excIllegalFunction: exc += "Illegal function!"; break;
				case Master.excIllegalDataAdr: exc += "Illegal data adress!"; break;
				case Master.excIllegalDataVal: exc += "Illegal data value!"; break;
				case Master.excSlaveDeviceFailure: exc += "Slave device failure!"; break;
				case Master.excAck: exc += "Acknoledge!"; break;
				case Master.excGatePathUnavailable: exc += "Gateway path unavailbale!"; break;
				case Master.excExceptionTimeout: exc += "Slave timed out!"; break;
				case Master.excExceptionConnectionLost: exc += "Connection is lost!"; break;
				case Master.excExceptionNotConnected: exc += "Not connected!"; break;
			}

			consoleText.text = exc;
		}

		private ushort ReadStartAdr()
		{
			if (txtStartAdress.text.IndexOf("0x", 0, txtStartAdress.text.Length) == 0)
			{
				string str = txtStartAdress.text.Replace("0x", "");
				ushort hex = Convert.ToUInt16(str, 16);
				return hex;
			}
			else
			{
				return Convert.ToUInt16(txtStartAdress.text);
			}
		}

	}
}
