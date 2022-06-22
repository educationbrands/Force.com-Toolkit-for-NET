namespace Salesforce.Common.Soql
{
	public interface ISelectListResolver
	{
		string GetFieldsList<T>();
	}
}
