﻿using GostCryptography.Asn1.Ber;
using GostCryptography.Properties;

namespace GostCryptography.Asn1.Gost.Gost_28147_89
{
	public sealed class Gost_28147_89_Mac : Asn1OctetString
	{
		public Gost_28147_89_Mac()
		{
		}

		public Gost_28147_89_Mac(byte[] data)
			: base(data)
		{
		}


		public override void Decode(Asn1BerDecodeBuffer buffer, bool explicitTagging, int implicitLength)
		{
			base.Decode(buffer, explicitTagging, implicitLength);

			if ((Length < 1) || (Length > 4))
			{
				throw ExceptionUtility.CryptographicException(Resource.Asn1ConsVioException, nameof(Length), Length);
			}
		}

		public override int Encode(Asn1BerEncodeBuffer buffer, bool explicitTagging)
		{
			if ((Length < 1) || (Length > 4))
			{
				throw ExceptionUtility.CryptographicException(Resource.Asn1ConsVioException, nameof(Length), Length);
			}

			var len = base.Encode(buffer, false);

			if (explicitTagging)
			{
				len += buffer.EncodeTagAndLength(Tag, len);
			}

			return len;
		}
	}
}