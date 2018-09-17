using System.Web;
using System.Web.Optimization;

namespace WeddingCD
{
    public class BundleConfig
    {
        // Pour plus d'informations sur le regroupement, visitez https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        //"~/Scripts/jquery-{version}.js",
                        "~/Scripts/theme-main.js",
                        "~/Scripts/jquery.sticky.js"
                        , "~/Scripts/jquery.nav.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilisez la version de développement de Modernizr pour le développement et l'apprentissage. Puis, une fois
            // prêt pour la production, utilisez l'outil de génération à l'adresse https://modernizr.com pour sélectionner uniquement les tests dont vous avez besoin.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/LibrariesJavascript").Include(
                      "~/Scripts/libs/bootstrap-validator/validator.min.js",
                      "~/Scripts/libs/gallery-grid/gallery-grid.js",
                      "~/Scripts/libs/magnific-popup/jquery.magnific-popup.min.js",
                      "~/Scripts/libs/owl-carousel/owl.carousel.min.js",
                      "~/Scripts/libs/stellar/jquery.stellar.min.js",
                      "~/Scripts/libs/TimeCircles-countdown/TimeCircles.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/theme-style.css",
                      "~/Content/libs/loading/rolling.css",
                      "~/Content/libs/magnific-popup/magnific-popup.css",
                      "~/Content/libs/owl-carousel/owl.carousel.css",
                      "~/Content/libs/owl-carousel/owl.theme.css",
                      "~/Content/libs/TimeCircles-countdown/TimeCircles.css",
                      "~/Content/colors/pink.css"));
        }
    }
}
