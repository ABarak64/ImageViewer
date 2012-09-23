using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ImageProviders;
using ImageProvidersTests.Properties;
using System.IO;

namespace ImageProvidersTests
{
    /// <summary>
    /// A class to test all relevant methods in the PinterestBoard class.
    /// </summary>
    [TestFixture]
    class PinterestBoardTests
    {
        /// <summary>
        /// Confirm that we are correctly finding the pin image urls from a pinterest board htmldocument object.
        /// </summary>
        [Test]
        public void PinImageUrlsTest()
        {
            List<Uri> expected = new List<Uri>
            {
                new Uri("http://media-cache-lt0.pinterest.com/upload/251286854177953342_8JhnoMC6_f.jpg"),
                new Uri("http://media-cache0.pinterest.com/upload/265571709246843002_B4WPNnUC_f.jpg"),
                new Uri("http://media-cache-ec5.pinterest.com/upload/218495019392755281_32uBAZ70_f.jpg"),
                new Uri("http://media-cache-ec2.pinterest.com/upload/210543351300567476_mgqLsVTI_f.jpg"),
                new Uri("http://media-cache-ec3.pinterest.com/upload/254523816410114620_ksYiV5q0_f.jpg"),
                new Uri("http://media-cache-ec5.pinterest.com/upload/229120699761953489_FbaprerB_f.jpg"),
                new Uri("http://media-cache-ec6.pinterest.com/upload/231161393343358413_QZ2Sd0vA_f.jpg"),
                new Uri("http://media-cache-ec3.pinterest.com/upload/250020216784426937_AHYzpAWt_f.jpg"),
                new Uri("http://media-cache-ec3.pinterest.com/upload/222646775298781559_Lj456pGy_f.jpg"),
                new Uri("http://media-cache-ec3.pinterest.com/upload/227080006181493479_BvTt4xY9_f.jpg"),
                new Uri("http://media-cache-ec2.pinterest.com/upload/198088083580855560_mYcwCcti_f.jpg"),
                new Uri("http://media-cache0.pinterest.com/upload/276338127106868484_CxSIzcKh_f.jpg"),
                new Uri("http://media-cache-ec3.pinterest.com/upload/186125397070353211_ou6kfH6W_f.jpg"),
                new Uri("http://media-cache-ec2.pinterest.com/upload/79164905919618394_lieNXoZS_f.jpg"),
                new Uri("http://media-cache-ec2.pinterest.com/upload/23573598020718911_2he0xPCI_f.jpg"),
                new Uri("http://media-cache0.pinterest.com/upload/236298311669434276_LxZoH8bD_f.jpg"),
                new Uri("http://media-cache-ec2.pinterest.com/upload/263460646921476246_MBd4rahH_f.jpg"),
                new Uri("http://media-cache-lt0.pinterest.com/upload/119556565077929075_1OOYyH3e_f.jpg"),
                new Uri("http://media-cache-ec6.pinterest.com/upload/27584616438676939_OBqBPrjx_f.jpg"),
                new Uri("http://media-cache-ec2.pinterest.com/upload/67483694386041825_Qcixqf9z_f.jpg"),
                new Uri("http://media-cache-ec2.pinterest.com/upload/18225573461957465_eum0ttIs_f.jpg"),
                new Uri("http://media-cache-ec2.pinterest.com/upload/90564642476202918_HeuOwioG_f.jpg"),
                new Uri("http://media-cache-ec5.pinterest.com/upload/83527768058469460_4obfCiKm_f.jpg"),
                new Uri("http://media-cache-ec3.pinterest.com/upload/22447698113564407_ScFF9YEE_f.jpg"),
                new Uri("http://media-cache-ec4.pinterest.com/upload/17803360996481638_IcwF0u5h_f.jpg"),
                new Uri("http://media-cache-ec6.pinterest.com/upload/227713324878970695_IreO4d9s_f.jpg"),
                new Uri("http://media-cache-ec4.pinterest.com/upload/215609900879855586_kXFMRgi3_f.jpg"),
                new Uri("http://media-cache-lt0.pinterest.com/upload/166492517445650419_FMxyPo1W_f.jpg"),
                new Uri("http://media-cache-ec5.pinterest.com/upload/139611657168665335_rTyYxWMp_f.jpg"),
                new Uri("http://media-cache-ec5.pinterest.com/upload/264093965619553206_GWGpBKiC_f.jpg"),
                new Uri("http://media-cache-ec2.pinterest.com/upload/118923246379853870_ZVs2xSpE_f.jpg"),
                new Uri("http://media-cache-ec6.pinterest.com/upload/201395414556781404_xav17l3h_f.jpg"),
                new Uri("http://media-cache-ec3.pinterest.com/upload/218424650647505418_re3IYohF_f.jpg"),
                new Uri("http://media-cache0.pinterest.com/upload/170362798374312177_v5FWhDCA_f.jpg"),
                new Uri("http://media-cache-lt0.pinterest.com/upload/173036810653158012_amGHoap0_f.jpg"),
                new Uri("http://media-cache-lt0.pinterest.com/upload/58828338853737264_F94k6WQM_f.jpg"),
                new Uri("http://media-cache-ec5.pinterest.com/upload/24699497927339150_0A8jcu36_f.jpg"),
                new Uri("http://media-cache-lt0.pinterest.com/upload/226587424971588897_KTJKzFkh_f.jpg"),
                new Uri("http://media-cache-ec6.pinterest.com/upload/147000375307337546_OELiwxBm_f.jpg"),
                new Uri("http://media-cache-ec6.pinterest.com/upload/30469734948424142_bXFn4oXj_f.jpg"),
                new Uri("http://media-cache-ec2.pinterest.com/upload/21110691973619968_AgIt8M8R_f.jpg"),
                new Uri("http://media-cache-ec4.pinterest.com/upload/218987600601003362_d6xPGpPf_f.jpg"),
                new Uri("http://media-cache-lt0.pinterest.com/upload/178455203954606843_lBhck3aC_f.jpg"),
                new Uri("http://media-cache-ec4.pinterest.com/upload/45387908715370890_8hpqWGrq_f.jpg"),
                new Uri("http://media-cache-ec5.pinterest.com/upload/78179743501731929_PsVgFuES_f.jpg"),
                new Uri("http://media-cache-ec4.pinterest.com/upload/11399805277101990_ujIeqJAw_f.jpg"),
                new Uri("http://media-cache-ec3.pinterest.com/upload/142356038192689977_h0IgKdSQ_f.jpg"),
                new Uri("http://media-cache0.pinterest.com/upload/284993482639035173_vwFVj6Ta_f.jpg"),
                new Uri("http://media-cache-ec3.pinterest.com/upload/257338566178852655_vGB06rz3_f.jpg"),
                new Uri("http://media-cache-lt0.pinterest.com/upload/168040629815881391_Bb1QhkkC_f.jpg")
            };

            string samplePinterestHtml = Resources.pinterestsamplepage;

            var page = new PinterestBoard(samplePinterestHtml);
            var actual = page.PinImageUrls;

            var differenceQuery = expected.Except(actual);
            Assert.AreEqual(differenceQuery.Count(), 0);
        }
    }
}
