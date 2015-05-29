using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Assets
{
	// A bigger unit was needed to work with the scale of unity. This set of classes
	// attempts to resolve this issue by creating a larger standard unit.

	// This is the mass of the sun. (S)un(M)ass.
	class SM
	{
		private int _mass = 0;

		public SM(int massInSM)
		{
			_mass = massInSM;
		}

		//public long GetMassAsKG()
		//{
		//	return _mass * 1989000000000000000000000000000;
		//}
	}
	// This is the radius of the sun. (R)adius(S)un.
	class RS
	{

		// The radius in RS.
		private int _radius = 0;

		public RS(int distanceInRS)
		{
			_radius = distanceInRS;
		}

		public int GetDistanceAsRS()
		{
			return _radius;
		}

		public int GetDistanceAsKM()
		{
			return _radius * 695800;
		}

		public int GetDistanceAsM()
		{
			return _radius * 695800000;
		}

		public void SetDistanceFromRS(int RS)
		{
			_radius = RS;
		}

		public void SetDistanceFromKM(int kilometers)
		{
			_radius = kilometers / 695800;
		}

		public void SetDistanceFromM(int meters)
		{
			_radius = meters / 695800000;
		}
	}

	// This is an int for dealing with really big numbers. Like the mass of the sun.
	class Int128
	{
		public Int128()
		{
			m_high_bits = 0;
			m_low_bits = 0;
		}


		// Allows conversions of longs to Int128's. This cannot be reversed.
		public static implicit operator Int128(long conversion)
		{
			Int128 cValue = new Int128();
			Int64 mask = 1 << 63;
			Int64 upperBits = conversion & mask;
			UInt64 lowerBits = (ulong)(Math.Abs(conversion));

			cValue.m_low_bits = lowerBits;
			cValue.m_high_bits = upperBits;

			return cValue;
		}

		// Allows the addition of two Int128's
		public Int128 operator+ (Int128 rhs)
		{
			Int128 sum = 0;

			sum.m_high_bits = this.m_high_bits + rhs.m_high_bits;
			sum.m_low_bits = this.m_low_bits + rhs.m_low_bits;

			// If a bit was lost to overflow..
			if (sum.m_low_bits < this.m_low_bits)
				++sum.m_high_bits;

			return sum;
		}

		// The upper bits of the number.
		Int64  m_high_bits;
		// The lower bits of the number.
		UInt64 m_low_bits;
	}
}
