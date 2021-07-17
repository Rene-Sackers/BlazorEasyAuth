using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace BlazorEasyAuth.Models
{
	public class Policy
	{
		private static readonly HashSet<Policy> Policies = new();

		public static IReadOnlyCollection<Policy> AllPolicies => Policies;

		public string Name { get; }
		
		public Policy([CallerMemberName] string name = null)
		{
			var policyMethod = new StackTrace().GetFrame(1)?.GetMethod();
			if (policyMethod?.DeclaringType == null)
				throw new("Failed to determine parent method for auto policy naming");
			
			Name = $"{policyMethod.DeclaringType.FullName}.{name}".Replace("+", ".");
			
			if (Policies.Contains(this))
				throw new InvalidOperationException("Duplicate policy registered: " + Name);
			
			Policies.Add(this);
		}

		public override string ToString()
			=> Name;

		public static implicit operator string(Policy policy)
			=> policy.ToString();

		public override bool Equals(object obj)
			=> obj is Policy otherPolicy && Equals(otherPolicy);

		private bool Equals(Policy other)
			=> Name == other.Name;

		public override int GetHashCode()
			=> Name != null ? Name.GetHashCode() : 0;
	}
}
