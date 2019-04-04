using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace GitProc.Api.Extensions.Globalization
{
    public class EFStringLocalizer : IStringLocalizer
    {
        private readonly GlobalizationDbContext _db;
        private Dictionary<string, Dictionary<string, LocalizedString>> _localizedStrings;

        public EFStringLocalizer(GlobalizationDbContext db)
        {
            _db = db;
            _localizedStrings = new Dictionary<string, Dictionary<string, LocalizedString>>();
        }

        public LocalizedString this[string name]
        {
            get
            {
                Initialize(CultureInfo.CurrentCulture);
                var cultureStrings = _localizedStrings[CultureInfo.CurrentCulture.Name];
                if (cultureStrings.ContainsKey(name))
                {
                    var format = cultureStrings[name];
                    return format;
                }

                return new LocalizedString(name, name);
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                Initialize(CultureInfo.CurrentCulture);
                var cultureStrings = _localizedStrings[CultureInfo.CurrentCulture.Name];
                if (cultureStrings.ContainsKey(name))
                {
                    var format = cultureStrings[name];
                    var value = string.Format(format ?? name, arguments);
                    return new LocalizedString(name, value, resourceNotFound: format == null);
                }

                return new LocalizedString(name, name);
            }
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            CultureInfo.DefaultThreadCurrentCulture = culture;
            return new EFStringLocalizer(_db);
        }

        private void Initialize(CultureInfo culture)
        {
            var currentCulture = culture.Name;

            if (!_localizedStrings.ContainsKey(currentCulture))
            {
                var dict = _db.StringResources
                    .AsNoTracking()
                    .Where(r => r.Culture.Name == currentCulture)
                    .Select(p => new { p.Key, Value = new LocalizedString(p.Key, p.Value) })
                    .ToDictionary(k => k.Key, v => v.Value);

                _localizedStrings.Add(currentCulture, dict);
            }
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeAncestorCultures)
        {
            Initialize(CultureInfo.CurrentCulture);
            return _localizedStrings[CultureInfo.CurrentCulture.Name].Values;
        }
    }

    public class EFStringLocalizer<T> : IStringLocalizer<T>
    {
        private readonly GlobalizationDbContext _db;

        public EFStringLocalizer(GlobalizationDbContext db)
        {
            _db = db;
        }

        public LocalizedString this[string name]
        {
            get
            {
                var value = GetString(name);
                return new LocalizedString(name, value ?? name, value == null);
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var format = GetString(name);
                var value = string.Format(format ?? name, arguments);
                return new LocalizedString(name, value, format == null);
            }
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            CultureInfo.DefaultThreadCurrentCulture = culture;
            return new EFStringLocalizer(_db);
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeAncestorCultures)
        {
            return Queryable.Where<StringResource>(_db.StringResources
                    .Include(r => r.Culture), r => r.Culture.Name == CultureInfo.CurrentCulture.Name)
                .Select(r => new LocalizedString(r.Key, r.Value, true));
        }

        private string GetString(string name)
        {
            return Queryable.Where<StringResource>(_db.StringResources
                    .Include(r => r.Culture), r => r.Culture.Name == CultureInfo.CurrentCulture.Name)
                .FirstOrDefault(r => r.Key == name)?.Value;
        }
    }
}
