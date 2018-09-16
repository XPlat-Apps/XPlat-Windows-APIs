namespace XPlat.UnitTests.iOS.Mocks
{
    using System;

    public class AppSetting
    {
        public string Name { get; set; }

        public DateTime Date { get; set; }

        public AppSetting NestedSetting { get; set; }

        public override bool Equals(object obj)
        {
            AppSetting appSetting = obj as AppSetting;
            return appSetting != null ? this.Equals(appSetting) : base.Equals(obj);
        }

        protected bool Equals(AppSetting other)
        {
            return string.Equals(this.Name, other.Name) && this.Date.Equals(other.Date)
                   && Equals(this.NestedSetting, other.NestedSetting);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (this.Name != null ? this.Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ this.Date.GetHashCode();
                hashCode = (hashCode * 397) ^ (this.NestedSetting != null ? this.NestedSetting.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}