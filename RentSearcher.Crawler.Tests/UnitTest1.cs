using RentSearcher.Crawler.Connectors;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RentSearcher.Crawler.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        const string detailsResponse = @"{
    ""_embedded"": {
        ""calculator"": null,
        ""favourite"": {
            ""_links"": {
                ""self"": {
                    ""href"": ""/cs/v2/favourite/1982965324""
                }
            },
            ""is_favourite"": false
        },
        ""images"": [
            {
                ""_links"": {
                    ""dynamicDown"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QL_Jt/tDgQQp.jpeg?fl=res,{width},{height},3|shr,,20|jpg,90""
                    },
                    ""dynamicUp"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QL_Jt/tDgQQp.jpeg?fl=res,{width},{height},3|wrm,/watermark/sreality.png,10|shr,,20|jpg,90""
                    },
                    ""gallery"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QL_Jt/tDgQQp.jpeg?fl=res,221,166,3|shr,,20|jpg,90""
                    },
                    ""self"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QL_Jt/tDgQQp.jpeg?fl=res,1920,1080,1|wrm,/watermark/sreality.png,10|shr,,20|jpg,90"",
                        ""title"": ""IMG_010""
                    },
                    ""view"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QL_Jt/tDgQQp.jpeg?fl=res,749,562,3|shr,,20|jpg,90""
                    }
                },
                ""id"": 633899284,
                ""kind"": 2,
                ""order"": 1
            },
            {
                ""_links"": {
                    ""dynamicDown"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QO_Ke/FkzQIx.jpeg?fl=res,{width},{height},3|shr,,20|jpg,90""
                    },
                    ""dynamicUp"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QO_Ke/FkzQIx.jpeg?fl=res,{width},{height},3|wrm,/watermark/sreality.png,10|shr,,20|jpg,90""
                    },
                    ""gallery"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QO_Ke/FkzQIx.jpeg?fl=res,221,166,3|shr,,20|jpg,90""
                    },
                    ""self"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QO_Ke/FkzQIx.jpeg?fl=res,1920,1080,1|wrm,/watermark/sreality.png,10|shr,,20|jpg,90"",
                        ""title"": ""IMG_002""
                    },
                    ""view"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QO_Ke/FkzQIx.jpeg?fl=res,749,562,3|shr,,20|jpg,90""
                    }
                },
                ""id"": 633899306,
                ""kind"": 2,
                ""order"": 4
            },
            {
                ""_links"": {
                    ""dynamicDown"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QN_Jr/6YbQRz.jpeg?fl=res,{width},{height},3|shr,,20|jpg,90""
                    },
                    ""dynamicUp"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QN_Jr/6YbQRz.jpeg?fl=res,{width},{height},3|wrm,/watermark/sreality.png,10|shr,,20|jpg,90""
                    },
                    ""gallery"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QN_Jr/6YbQRz.jpeg?fl=res,221,166,3|shr,,20|jpg,90""
                    },
                    ""self"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QN_Jr/6YbQRz.jpeg?fl=res,1920,1080,1|wrm,/watermark/sreality.png,10|shr,,20|jpg,90"",
                        ""title"": ""IMG_012""
                    },
                    ""view"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QN_Jr/6YbQRz.jpeg?fl=res,749,562,3|shr,,20|jpg,90""
                    }
                },
                ""id"": 633899317,
                ""kind"": 2,
                ""order"": 5
            },
            {
                ""_links"": {
                    ""dynamicDown"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QM_KO/HazQJp.jpeg?fl=res,{width},{height},3|shr,,20|jpg,90""
                    },
                    ""dynamicUp"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QM_KO/HazQJp.jpeg?fl=res,{width},{height},3|wrm,/watermark/sreality.png,10|shr,,20|jpg,90""
                    },
                    ""gallery"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QM_KO/HazQJp.jpeg?fl=res,221,166,3|shr,,20|jpg,90""
                    },
                    ""self"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QM_KO/HazQJp.jpeg?fl=res,1920,1080,1|wrm,/watermark/sreality.png,10|shr,,20|jpg,90"",
                        ""title"": ""3+kk_3d_planek_viz""
                    },
                    ""view"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QM_KO/HazQJp.jpeg?fl=res,749,562,3|shr,,20|jpg,90""
                    }
                },
                ""id"": 633899328,
                ""kind"": 4,
                ""order"": 6
            },
            {
                ""_links"": {
                    ""dynamicDown"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QN_Jr/bROQR0.jpeg?fl=res,{width},{height},3|shr,,20|jpg,90""
                    },
                    ""dynamicUp"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QN_Jr/bROQR0.jpeg?fl=res,{width},{height},3|wrm,/watermark/sreality.png,10|shr,,20|jpg,90""
                    },
                    ""gallery"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QN_Jr/bROQR0.jpeg?fl=res,221,166,3|shr,,20|jpg,90""
                    },
                    ""self"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QN_Jr/bROQR0.jpeg?fl=res,1920,1080,1|wrm,/watermark/sreality.png,10|shr,,20|jpg,90"",
                        ""title"": ""IMG_005""
                    },
                    ""view"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QN_Jr/bROQR0.jpeg?fl=res,749,562,3|shr,,20|jpg,90""
                    }
                },
                ""id"": 633899339,
                ""kind"": 2,
                ""order"": 7
            },
            {
                ""_links"": {
                    ""dynamicDown"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QI_JW/BUqQRF.jpeg?fl=res,{width},{height},3|shr,,20|jpg,90""
                    },
                    ""dynamicUp"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QI_JW/BUqQRF.jpeg?fl=res,{width},{height},3|wrm,/watermark/sreality.png,10|shr,,20|jpg,90""
                    },
                    ""gallery"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QI_JW/BUqQRF.jpeg?fl=res,221,166,3|shr,,20|jpg,90""
                    },
                    ""self"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QI_JW/BUqQRF.jpeg?fl=res,1920,1080,1|wrm,/watermark/sreality.png,10|shr,,20|jpg,90"",
                        ""title"": ""IMG_018""
                    },
                    ""view"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QI_JW/BUqQRF.jpeg?fl=res,749,562,3|shr,,20|jpg,90""
                    }
                },
                ""id"": 633899350,
                ""kind"": 2,
                ""order"": 8
            },
            {
                ""_links"": {
                    ""dynamicDown"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QL_Jt/qltQQq.jpeg?fl=res,{width},{height},3|shr,,20|jpg,90""
                    },
                    ""dynamicUp"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QL_Jt/qltQQq.jpeg?fl=res,{width},{height},3|wrm,/watermark/sreality.png,10|shr,,20|jpg,90""
                    },
                    ""gallery"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QL_Jt/qltQQq.jpeg?fl=res,221,166,3|shr,,20|jpg,90""
                    },
                    ""self"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QL_Jt/qltQQq.jpeg?fl=res,1920,1080,1|wrm,/watermark/sreality.png,10|shr,,20|jpg,90"",
                        ""title"": ""IMG_008""
                    },
                    ""view"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QL_Jt/qltQQq.jpeg?fl=res,749,562,3|shr,,20|jpg,90""
                    }
                },
                ""id"": 633899361,
                ""kind"": 2,
                ""order"": 9
            },
            {
                ""_links"": {
                    ""dynamicDown"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QI_JW/iO9QRG.jpeg?fl=res,{width},{height},3|shr,,20|jpg,90""
                    },
                    ""dynamicUp"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QI_JW/iO9QRG.jpeg?fl=res,{width},{height},3|wrm,/watermark/sreality.png,10|shr,,20|jpg,90""
                    },
                    ""gallery"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QI_JW/iO9QRG.jpeg?fl=res,221,166,3|shr,,20|jpg,90""
                    },
                    ""self"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QI_JW/iO9QRG.jpeg?fl=res,1920,1080,1|wrm,/watermark/sreality.png,10|shr,,20|jpg,90"",
                        ""title"": ""IMG_019""
                    },
                    ""view"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QI_JW/iO9QRG.jpeg?fl=res,749,562,3|shr,,20|jpg,90""
                    }
                },
                ""id"": 633899372,
                ""kind"": 2,
                ""order"": 10
            },
            {
                ""_links"": {
                    ""dynamicDown"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QQ_LR/Ml2QMH.jpeg?fl=res,{width},{height},3|shr,,20|jpg,90""
                    },
                    ""dynamicUp"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QQ_LR/Ml2QMH.jpeg?fl=res,{width},{height},3|wrm,/watermark/sreality.png,10|shr,,20|jpg,90""
                    },
                    ""gallery"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QQ_LR/Ml2QMH.jpeg?fl=res,221,166,3|shr,,20|jpg,90""
                    },
                    ""self"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QQ_LR/Ml2QMH.jpeg?fl=res,1920,1080,1|wrm,/watermark/sreality.png,10|shr,,20|jpg,90"",
                        ""title"": ""IMG_015""
                    },
                    ""view"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QQ_LR/Ml2QMH.jpeg?fl=res,749,562,3|shr,,20|jpg,90""
                    }
                },
                ""id"": 633899383,
                ""kind"": 2,
                ""order"": 11
            },
            {
                ""_links"": {
                    ""dynamicDown"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QP_Kn/vZyQOU.jpeg?fl=res,{width},{height},3|shr,,20|jpg,90""
                    },
                    ""dynamicUp"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QP_Kn/vZyQOU.jpeg?fl=res,{width},{height},3|wrm,/watermark/sreality.png,10|shr,,20|jpg,90""
                    },
                    ""gallery"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QP_Kn/vZyQOU.jpeg?fl=res,221,166,3|shr,,20|jpg,90""
                    },
                    ""self"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QP_Kn/vZyQOU.jpeg?fl=res,1920,1080,1|wrm,/watermark/sreality.png,10|shr,,20|jpg,90"",
                        ""title"": ""IMG_025""
                    },
                    ""view"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QP_Kn/vZyQOU.jpeg?fl=res,749,562,3|shr,,20|jpg,90""
                    }
                },
                ""id"": 633899405,
                ""kind"": 2,
                ""order"": 12
            },
            {
                ""_links"": {
                    ""dynamicDown"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QN_Jr/X5bQR1.jpeg?fl=res,{width},{height},3|shr,,20|jpg,90""
                    },
                    ""dynamicUp"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QN_Jr/X5bQR1.jpeg?fl=res,{width},{height},3|wrm,/watermark/sreality.png,10|shr,,20|jpg,90""
                    },
                    ""gallery"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QN_Jr/X5bQR1.jpeg?fl=res,221,166,3|shr,,20|jpg,90""
                    },
                    ""self"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QN_Jr/X5bQR1.jpeg?fl=res,1920,1080,1|wrm,/watermark/sreality.png,10|shr,,20|jpg,90"",
                        ""title"": ""IMG_023""
                    },
                    ""view"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QN_Jr/X5bQR1.jpeg?fl=res,749,562,3|shr,,20|jpg,90""
                    }
                },
                ""id"": 633899427,
                ""kind"": 2,
                ""order"": 13
            },
            {
                ""_links"": {
                    ""dynamicDown"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QO_Ke/YJjQIy.jpeg?fl=res,{width},{height},3|shr,,20|jpg,90""
                    },
                    ""dynamicUp"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QO_Ke/YJjQIy.jpeg?fl=res,{width},{height},3|wrm,/watermark/sreality.png,10|shr,,20|jpg,90""
                    },
                    ""gallery"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QO_Ke/YJjQIy.jpeg?fl=res,221,166,3|shr,,20|jpg,90""
                    },
                    ""self"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QO_Ke/YJjQIy.jpeg?fl=res,1920,1080,1|wrm,/watermark/sreality.png,10|shr,,20|jpg,90"",
                        ""title"": ""IMG_020""
                    },
                    ""view"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QO_Ke/YJjQIy.jpeg?fl=res,749,562,3|shr,,20|jpg,90""
                    }
                },
                ""id"": 633899449,
                ""kind"": 2,
                ""order"": 14
            },
            {
                ""_links"": {
                    ""dynamicDown"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QJ_Jb/GkEQNv.jpeg?fl=res,{width},{height},3|shr,,20|jpg,90""
                    },
                    ""dynamicUp"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QJ_Jb/GkEQNv.jpeg?fl=res,{width},{height},3|wrm,/watermark/sreality.png,10|shr,,20|jpg,90""
                    },
                    ""gallery"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QJ_Jb/GkEQNv.jpeg?fl=res,221,166,3|shr,,20|jpg,90""
                    },
                    ""self"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QJ_Jb/GkEQNv.jpeg?fl=res,1920,1080,1|wrm,/watermark/sreality.png,10|shr,,20|jpg,90"",
                        ""title"": ""IMG_014""
                    },
                    ""view"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QJ_Jb/GkEQNv.jpeg?fl=res,749,562,3|shr,,20|jpg,90""
                    }
                },
                ""id"": 633899471,
                ""kind"": 2,
                ""order"": 15
            },
            {
                ""_links"": {
                    ""dynamicDown"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QR_L1/7wjQQy.jpeg?fl=res,{width},{height},3|shr,,20|jpg,90""
                    },
                    ""dynamicUp"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QR_L1/7wjQQy.jpeg?fl=res,{width},{height},3|wrm,/watermark/sreality.png,10|shr,,20|jpg,90""
                    },
                    ""gallery"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QR_L1/7wjQQy.jpeg?fl=res,221,166,3|shr,,20|jpg,90""
                    },
                    ""self"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QR_L1/7wjQQy.jpeg?fl=res,1920,1080,1|wrm,/watermark/sreality.png,10|shr,,20|jpg,90"",
                        ""title"": ""IMG_033""
                    },
                    ""view"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QR_L1/7wjQQy.jpeg?fl=res,749,562,3|shr,,20|jpg,90""
                    }
                },
                ""id"": 633899482,
                ""kind"": 2,
                ""order"": 16
            },
            {
                ""_links"": {
                    ""dynamicDown"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QJ_Jb/IbhQNw.jpeg?fl=res,{width},{height},3|shr,,20|jpg,90""
                    },
                    ""dynamicUp"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QJ_Jb/IbhQNw.jpeg?fl=res,{width},{height},3|wrm,/watermark/sreality.png,10|shr,,20|jpg,90""
                    },
                    ""gallery"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QJ_Jb/IbhQNw.jpeg?fl=res,221,166,3|shr,,20|jpg,90""
                    },
                    ""self"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QJ_Jb/IbhQNw.jpeg?fl=res,1920,1080,1|wrm,/watermark/sreality.png,10|shr,,20|jpg,90"",
                        ""title"": ""IMG_026""
                    },
                    ""view"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QJ_Jb/IbhQNw.jpeg?fl=res,749,562,3|shr,,20|jpg,90""
                    }
                },
                ""id"": 633899504,
                ""kind"": 2,
                ""order"": 17
            },
            {
                ""_links"": {
                    ""dynamicDown"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QQ_LR/2XiQMI.jpeg?fl=res,{width},{height},3|shr,,20|jpg,90""
                    },
                    ""dynamicUp"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QQ_LR/2XiQMI.jpeg?fl=res,{width},{height},3|wrm,/watermark/sreality.png,10|shr,,20|jpg,90""
                    },
                    ""gallery"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QQ_LR/2XiQMI.jpeg?fl=res,221,166,3|shr,,20|jpg,90""
                    },
                    ""self"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QQ_LR/2XiQMI.jpeg?fl=res,1920,1080,1|wrm,/watermark/sreality.png,10|shr,,20|jpg,90"",
                        ""title"": ""IMG_030""
                    },
                    ""view"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QQ_LR/2XiQMI.jpeg?fl=res,749,562,3|shr,,20|jpg,90""
                    }
                },
                ""id"": 633899526,
                ""kind"": 2,
                ""order"": 18
            },
            {
                ""_links"": {
                    ""dynamicDown"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QQ_LR/7qeQMJ.jpeg?fl=res,{width},{height},3|shr,,20|jpg,90""
                    },
                    ""dynamicUp"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QQ_LR/7qeQMJ.jpeg?fl=res,{width},{height},3|wrm,/watermark/sreality.png,10|shr,,20|jpg,90""
                    },
                    ""gallery"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QQ_LR/7qeQMJ.jpeg?fl=res,221,166,3|shr,,20|jpg,90""
                    },
                    ""self"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QQ_LR/7qeQMJ.jpeg?fl=res,1920,1080,1|wrm,/watermark/sreality.png,10|shr,,20|jpg,90"",
                        ""title"": ""IMG_032""
                    },
                    ""view"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QQ_LR/7qeQMJ.jpeg?fl=res,749,562,3|shr,,20|jpg,90""
                    }
                },
                ""id"": 633899548,
                ""kind"": 2,
                ""order"": 19
            },
            {
                ""_links"": {
                    ""dynamicDown"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QP_Kn/q9TQOV.jpeg?fl=res,{width},{height},3|shr,,20|jpg,90""
                    },
                    ""dynamicUp"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QP_Kn/q9TQOV.jpeg?fl=res,{width},{height},3|wrm,/watermark/sreality.png,10|shr,,20|jpg,90""
                    },
                    ""gallery"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QP_Kn/q9TQOV.jpeg?fl=res,221,166,3|shr,,20|jpg,90""
                    },
                    ""self"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QP_Kn/q9TQOV.jpeg?fl=res,1920,1080,1|wrm,/watermark/sreality.png,10|shr,,20|jpg,90"",
                        ""title"": ""IMG_035""
                    },
                    ""view"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QP_Kn/q9TQOV.jpeg?fl=res,749,562,3|shr,,20|jpg,90""
                    }
                },
                ""id"": 633899559,
                ""kind"": 2,
                ""order"": 20
            },
            {
                ""_links"": {
                    ""dynamicDown"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QN_Jr/KZ0QR2.jpeg?fl=res,{width},{height},3|shr,,20|jpg,90""
                    },
                    ""dynamicUp"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QN_Jr/KZ0QR2.jpeg?fl=res,{width},{height},3|wrm,/watermark/sreality.png,10|shr,,20|jpg,90""
                    },
                    ""gallery"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QN_Jr/KZ0QR2.jpeg?fl=res,221,166,3|shr,,20|jpg,90""
                    },
                    ""self"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QN_Jr/KZ0QR2.jpeg?fl=res,1920,1080,1|wrm,/watermark/sreality.png,10|shr,,20|jpg,90"",
                        ""title"": ""IMG_037""
                    },
                    ""view"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QN_Jr/KZ0QR2.jpeg?fl=res,749,562,3|shr,,20|jpg,90""
                    }
                },
                ""id"": 633899581,
                ""kind"": 2,
                ""order"": 21
            },
            {
                ""_links"": {
                    ""dynamicDown"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QK_JW/7U5QUN.jpeg?fl=res,{width},{height},3|shr,,20|jpg,90""
                    },
                    ""dynamicUp"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QK_JW/7U5QUN.jpeg?fl=res,{width},{height},3|wrm,/watermark/sreality.png,10|shr,,20|jpg,90""
                    },
                    ""gallery"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QK_JW/7U5QUN.jpeg?fl=res,221,166,3|shr,,20|jpg,90""
                    },
                    ""self"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QK_JW/7U5QUN.jpeg?fl=res,1920,1080,1|wrm,/watermark/sreality.png,10|shr,,20|jpg,90"",
                        ""title"": ""IMG_036""
                    },
                    ""view"": {
                        ""href"": ""https://d18-a.sdn.cz/d_18/c_img_QK_JW/7U5QUN.jpeg?fl=res,749,562,3|shr,,20|jpg,90""
                    }
                },
                ""id"": 633899603,
                ""kind"": 2,
                ""order"": 22
            }
        ],
        ""matterport_url"": """",
        ""note"": {
            ""_links"": {
                ""self"": {
                    ""href"": ""/cs/v2/note/1982965324""
                }
            },
            ""has_note"": false,
            ""note"": """"
        },
        ""seller"": {
            ""_embedded"": {
                ""premise"": {
                    ""_links"": {
                        ""self"": {
                            ""href"": ""/cs/v2/companies/18670"",
                            ""profile"": ""/companies/1/doc""
                        }
                    },
                    ""address"": ""Pod Žvahovem 103/29, 15200 Praha - Hlubočepy"",
                    ""allow_calculator"": 1,
                    ""ask"": {
                        ""addr_city"": ""Praha, Hlubočepy"",
                        ""addr_house_num"": ""103/29"",
                        ""addr_street"": ""Pod Žvahovem"",
                        ""addr_zip"": ""15200"",
                        ""address"": ""Pod Žvahovem 103/29, 152 00  Praha, Hlubočepy"",
                        ""description"": ""Zabýváme se zprostředkováním prodeje nemovitostí, realizujeme developerské projekty."",
                        ""email"": ""info@capitas.cz"",
                        ""emails"": [
                            {
                                ""email"": ""info@capitas.cz"",
                                ""role"": ""E-mail""
                            }
                        ],
                        ""firmy_review_url"": ""https://www.firmy.cz/detail/13167425-capitast-inv-s-r-o-praha-hlubocepy.html#pridat-hodnoceni"",
                        ""is_paid"": false,
                        ""phones"": [
                            {
                                ""country_code"": ""420"",
                                ""number"": ""777568255"",
                                ""role"": ""Mobil""
                            }
                        ]
                    },
                    ""ask_id"": 13167425,
                    ""company_id"": 11433,
                    ""company_paid_firmy"": 0,
                    ""company_subject_id"": 4041779,
                    ""description"": ""Zabýváme se zprostředkováním prodeje nemovitostí, realizujeme developerské projekty."",
                    ""email"": ""info@capitas.cz"",
                    ""ico"": 7326530,
                    ""id"": 18670,
                    ""locality"": {
                        ""lat"": 50.0775130093508,
                        ""lon"": 14.4294150508725
                    },
                    ""logo"": ""https://d48-a.sdn.cz/d_48/c_img_F_F/37dDlC.png?fl=res,140,140,3,ffffff"",
                    ""logo_small"": ""https://d48-a.sdn.cz/d_48/c_img_F_F/37dDlC.png?fl=res,70,70,1,ffffff"",
                    ""name"": ""Capitast Inv, s.r.o."",
                    ""phones"": [
                        {
                            ""code"": ""420"",
                            ""number"": ""777568255"",
                            ""type"": ""TEL""
                        }
                    ],
                    ""poi_logo"": """",
                    ""retargeting_id"": 0,
                    ""seznam_naplno"": 0,
                    ""url"": ""Capitast-Inv-s-r-o-Praha-Hlubocepy"",
                    ""www"": """",
                    ""www_visible"": """"
                }
            },
            ""_links"": {
                ""self"": {
                    ""href"": ""/cs/v2/seller/88610"",
                    ""profile"": ""/seller/1/doc""
                }
            },
            ""active"": true,
            ""ask"": {},
            ""broker_ico"": 0,
            ""broker_tip_description"": """",
            ""broker_video"": {},
            ""certificates"": [],
            ""email"": ""katerina.spilackova@capitas.cz"",
            ""image"": ""https:"",
            ""image_dynamic"": """",
            ""in_banner_seller"": false,
            ""offers"": [],
            ""phones"": [
                {
                    ""code"": ""420"",
                    ""number"": ""777568255"",
                    ""type"": ""MOB""
                }
            ],
            ""specialization"": {
                ""category"": [
                    {
                        ""category_main_cb"": 1,
                        ""num"": 1
                    },
                    {
                        ""category_main_cb"": 2,
                        ""num"": 1
                    }
                ],
                ""type"": [
                    {
                        ""category_type_cb"": 1,
                        ""num"": 1
                    },
                    {
                        ""category_type_cb"": 2,
                        ""num"": 1
                    }
                ]
            },
            ""specialization_string"": """",
            ""user_id"": 88610,
            ""user_name"": ""Kateřina Špiláčková""
        }
    },
    ""_links"": {
        ""broader_search"": {
            ""href"": ""/cs/v2/estates?category_main_cb=1&locality_district_id=5005&category_type_cb=2"",
            ""title"": ""Pronájem bytů Praha 5""
        },
        ""local_search"": {
            ""href"": ""/cs/v2/estates?category_main_cb=1&region=Praha&region_entity_type=osmm&category_type_cb=2"",
            ""title"": ""Pronájem bytů Praha""
        },
        ""self"": {
            ""href"": ""/cs/v2/estates/1982965324"",
            ""profile"": ""/estates/1/doc"",
            ""title"": ""Detail inzeratu""
        },
        ""similar_adverts"": {
            ""href"": ""/cs/v2/estates?category_main_cb=1&locality_district_id=5005&category_sub_cb=6&category_type_cb=2"",
            ""title"": ""Pronájem bytů 3+kk Praha 5""
        }
    },
    ""codeItems"": {
        ""building_type_search"": 2,
        ""ownership"": 1,
        ""something_more1"": [
            3110,
            3120
        ],
        ""something_more2"": [
            3150
        ],
        ""something_more3"": [
            3310
        ]
    },
    ""is_topped"": true,
    ""is_topped_today"": true,
    ""items"": [
        {
            ""currency"": ""Kč"",
            ""name"": ""Celková cena"",
            ""negotiation"": false,
            ""notes"": [],
            ""type"": ""price_czk"",
            ""unit"": ""za nemovitost"",
            ""value"": ""30 000""
        },
        {
            ""name"": ""Poznámka k ceně"",
            ""type"": ""string"",
            ""value"": ""+ energie a poplatky, + provize RK""
        },
        {
            ""name"": ""ID zakázky"",
            ""type"": ""string"",
            ""value"": ""00064""
        },
        {
            ""name"": ""Aktualizace"",
            ""topped"": true,
            ""type"": ""edited"",
            ""value"": ""Dnes""
        },
        {
            ""name"": ""Stavba"",
            ""type"": ""string"",
            ""value"": ""Cihlová""
        },
        {
            ""name"": ""Stav objektu"",
            ""type"": ""string"",
            ""value"": ""Novostavba""
        },
        {
            ""name"": ""Vlastnictví"",
            ""type"": ""string"",
            ""value"": ""Osobní""
        },
        {
            ""name"": ""Podlaží"",
            ""type"": ""string"",
            ""value"": ""4. podlaží""
        },
        {
            ""name"": ""Užitná plocha"",
            ""type"": ""area"",
            ""unit"": ""m2"",
            ""value"": ""81""
        },
        {
            ""name"": ""Terasa"",
            ""type"": ""area"",
            ""unit"": ""m2"",
            ""value"": ""86""
        },
        {
            ""name"": ""Sklep"",
            ""type"": ""area"",
            ""unit"": ""m2"",
            ""value"": ""3""
        },
        {
            ""name"": ""Garáž"",
            ""type"": ""count"",
            ""value"": ""1""
        },
        {
            ""name"": ""Datum nastěhování"",
            ""type"": ""string"",
            ""value"": ""Ihned""
        },
        {
            ""name"": ""Voda"",
            ""type"": ""set"",
            ""value"": [
                {
                    ""name"": ""Voda"",
                    ""value"": ""Dálkový vodovod""
                }
            ]
        },
        {
            ""name"": ""Topení"",
            ""type"": ""set"",
            ""value"": [
                {
                    ""name"": ""Topení"",
                    ""value"": ""Ústřední dálkové""
                }
            ]
        },
        {
            ""name"": ""Odpad"",
            ""type"": ""set"",
            ""value"": [
                {
                    ""name"": ""Odpad"",
                    ""value"": ""Veřejná kanalizace""
                }
            ]
        },
        {
            ""name"": ""Telekomunikace"",
            ""type"": ""set"",
            ""value"": [
                {
                    ""name"": ""Telekomunikace"",
                    ""value"": ""Internet""
                },
                {
                    ""name"": ""Telekomunikace"",
                    ""value"": ""Kabelová televize""
                },
                {
                    ""name"": ""Telekomunikace"",
                    ""value"": ""Kabelové rozvody""
                }
            ]
        },
        {
            ""name"": ""Elektřina"",
            ""type"": ""set"",
            ""value"": [
                {
                    ""name"": ""Elektřina"",
                    ""value"": ""230V""
                }
            ]
        },
        {
            ""name"": ""Doprava"",
            ""type"": ""set"",
            ""value"": [
                {
                    ""name"": ""Doprava"",
                    ""value"": ""MHD""
                }
            ]
        },
        {
            ""name"": ""Komunikace"",
            ""type"": ""set"",
            ""value"": [
                {
                    ""name"": ""Komunikace"",
                    ""value"": ""Betonová""
                },
                {
                    ""name"": ""Komunikace"",
                    ""value"": ""Dlážděná""
                },
                {
                    ""name"": ""Komunikace"",
                    ""value"": ""Asfaltová""
                }
            ]
        },
        {
            ""name"": ""Energetická náročnost budovy"",
            ""type"": ""energy_efficiency_rating"",
            ""value"": ""Třída G - Mimořádně nehospodárná"",
            ""value_type"": ""G""
        },
        {
            ""name"": ""Vybavení"",
            ""type"": ""string"",
            ""value"": ""Částečně""
        },
        {
            ""name"": ""Výtah"",
            ""type"": ""boolean"",
            ""value"": true
        }
    ],
    ""locality"": {
        ""accuracy"": ""not_address"",
        ""name"": ""Adresa"",
        ""value"": ""Svitákova, Praha 5 - Stodůlky""
    },
    ""locality_district_id"": 5005,
    ""logged_in"": false,
    ""map"": {
        ""bounding_box"": {
            ""leftBottomBounding"": {
                ""lat"": 50.0454893993023,
                ""lon"": 14.3018067727598
            },
            ""rightTopBounding"": {
                ""lat"": 50.0474458737866,
                ""lon"": 14.3042872356062
            }
        },
        ""geometry"": [
            {
                ""data"": [
                    [
                        50.047243901797,
                        14.3018067727598
                    ],
                    [
                        50.0472308732941,
                        14.30203323166
                    ],
                    [
                        50.0472662392836,
                        14.3023805042236
                    ],
                    [
                        50.0472824595942,
                        14.3025353060476
                    ]
                ],
                ""type"": ""linestring""
            },
            {
                ""data"": [
                    [
                        50.0472824595942,
                        14.3025353060476
                    ],
                    [
                        50.0472987803861,
                        14.3027068672751
                    ],
                    [
                        50.0473122099921,
                        14.3028463457054
                    ],
                    [
                        50.047364144844,
                        14.3034070794761
                    ],
                    [
                        50.0474141778231,
                        14.3039510811551
                    ],
                    [
                        50.0474314544715,
                        14.3041324066361
                    ],
                    [
                        50.0474458737866,
                        14.3042872356062
                    ]
                ],
                ""type"": ""linestring""
            },
            {
                ""data"": [
                    [
                        50.0472824595942,
                        14.3025353060476
                    ],
                    [
                        50.0471899543583,
                        14.3025575975337
                    ],
                    [
                        50.0466762081523,
                        14.3026767793349
                    ],
                    [
                        50.0465908807475,
                        14.3026961728433
                    ],
                    [
                        50.0464741186891,
                        14.3027230052323
                    ],
                    [
                        50.0462819050658,
                        14.3027662942641
                    ],
                    [
                        50.0461274130069,
                        14.3028006563424
                    ],
                    [
                        50.0460348993083,
                        14.3028215501899
                    ],
                    [
                        50.0459477815934,
                        14.3028423657945
                    ],
                    [
                        50.0458139573506,
                        14.3028736346487
                    ],
                    [
                        50.0456971952319,
                        14.3029004661553
                    ],
                    [
                        50.045624441639,
                        14.3029168833831
                    ],
                    [
                        50.0454893993023,
                        14.3028950957203
                    ]
                ],
                ""type"": ""linestring""
            }
        ],
        ""lat"": 50.0470602778,
        ""lon"": 14.3026641667,
        ""type"": ""geometry"",
        ""zoom"": 15
    },
    ""meta_description"": ""Byt 3+kk 81 m² k pronájmu Svitákova, Praha 5 - Stodůlky; 30 000 Kč (+ energie a poplatky, + provize RK), terasa, garáž, výtah, cihlová stavba, osobní vlastnictví, novostavby."",
    ""name"": {
        ""name"": ""Název"",
        ""value"": ""Pronájem bytu 3+kk 81 m²""
    },
    ""panorama"": 0,
    ""poi"": [
        {
            ""description"": ""Dětské hřiště Bessemerova"",
            ""distance"": 92.0,
            ""imgUrl"": ""https://api.mapy.cz/poiimg/icon/204"",
            ""index"": 1,
            ""lat"": 50.0480907231,
            ""lines"": [],
            ""lon"": 14.3032331427,
            ""name"": ""Hřiště"",
            ""photo_url"": ""https://d18-a.sdn.cz/d_18/c_img_gT_n/jvRndx.jpeg?fl=res,300,300,3|shr,,20|jpg,90"",
            ""rating"": -1,
            ""review_count"": 0,
            ""source"": ""base"",
            ""source_id"": 1926677,
            ""url"": ""https://mapy.cz/zakladni?x=14.3032331427&y=50.0480907231&z=17&source=base&id=1926677""
        },
        {
            ""description"": ""Potraviny Svitákova 7"",
            ""distance"": 115.0,
            ""imgUrl"": ""https://api.mapy.cz/poiimg/icon/423"",
            ""index"": 1,
            ""lat"": 50.04705047607422,
            ""lines"": [],
            ""lon"": 14.302352905273438,
            ""name"": ""Večerka"",
            ""photo_url"": ""//d48-a.sdn.cz/d_48/c_img_gR_m/ZqlLTx.jpeg?fl=res,{width},{height},3"",
            ""rating"": 4,
            ""review_count"": 1,
            ""source"": ""firm"",
            ""source_id"": 13163774,
            ""url"": ""https://www.firmy.cz/detail/13163774-potraviny-svitakova-7-praha-stodulky.html""
        },
        {
            ""description"": ""Veterinární klinika Delta MVDr.Michael Růžička, s.r.o."",
            ""distance"": 1581.0,
            ""imgUrl"": ""https://api.mapy.cz/poiimg/icon/164"",
            ""index"": 1,
            ""lat"": 50.061885833740234,
            ""lines"": [],
            ""lon"": 14.307043075561523,
            ""name"": ""Veterinář"",
            ""photo_url"": ""//d48-a.sdn.cz/d_48/c_img_G_C/ZcGBh5O.jpeg?fl=res,{width},{height},3"",
            ""rating"": 3,
            ""review_count"": 8,
            ""source"": ""firm"",
            ""source_id"": 12957413,
            ""url"": ""https://www.firmy.cz/detail/12957413-veterinarni-klinika-delta-mvdr-michael-ruzicka-praha-repy.html""
        },
        {
            ""description"": ""La Zmrzka"",
            ""distance"": 237.0,
            ""imgUrl"": ""https://api.mapy.cz/poiimg/icon/38"",
            ""index"": 1,
            ""lat"": 50.046173095703125,
            ""lines"": [],
            ""lon"": 14.303495407104492,
            ""name"": ""Cukrárna"",
            ""photo_url"": ""//d48-a.sdn.cz/d_48/c_img_H_D/FbpCEGd.jpeg?fl=res,{width},{height},3"",
            ""rating"": 5,
            ""review_count"": 7,
            ""source"": ""firm"",
            ""source_id"": 13065320,
            ""url"": ""https://www.firmy.cz/detail/13065320-la-zmrzka-praha-stodulky.html""
        },
        {
            ""description"": ""G Pivnice Stodůlky"",
            ""distance"": 699.0,
            ""imgUrl"": ""https://api.mapy.cz/poiimg/icon/362"",
            ""index"": 1,
            ""lat"": 50.04658889770508,
            ""lines"": [],
            ""lon"": 14.31146240234375,
            ""name"": ""Hospoda"",
            ""photo_url"": ""//d48-a.sdn.cz/d_48/c_img_F_G/eVBXKa.jpeg?fl=res,{width},{height},3"",
            ""rating"": -1,
            ""review_count"": 0,
            ""source"": ""firm"",
            ""source_id"": 13146250,
            ""url"": ""https://www.firmy.cz/detail/13146250-g-pivnice-stodulky-praha-stodulky.html""
        },
        {
            ""description"": ""Divadelní studio Bubec"",
            ""distance"": 1377.0,
            ""imgUrl"": ""https://api.mapy.cz/poiimg/icon/39"",
            ""index"": 1,
            ""lat"": 50.036521911621094,
            ""lines"": [],
            ""lon"": 14.308954238891602,
            ""name"": ""Divadlo"",
            ""photo_url"": ""//d48-a.sdn.cz/d_48/c_img_gX_n/2fdLPP.jpeg?fl=res,{width},{height},3"",
            ""rating"": 2,
            ""review_count"": 1,
            ""source"": ""firm"",
            ""source_id"": 13047862,
            ""url"": ""https://www.firmy.cz/detail/13047862-divadelni-studio-bubec-praha-reporyje.html""
        },
        {
            ""description"": ""Kovářovic mez"",
            ""distance"": 4247.0,
            ""imgUrl"": ""https://api.mapy.cz/poiimg/icon/368"",
            ""index"": 1,
            ""lat"": 50.04395254566995,
            ""lines"": [],
            ""lon"": 14.36107759920759,
            ""name"": ""Přírodní zajímavost"",
            ""photo_url"": ""https://d34-a.sdn.cz/d_34/c_img_QI_f/x1u18.jpeg?fl=cro,0,524,4032,1250%7Cres,{width},{height},3"",
            ""rating"": -1,
            ""review_count"": 0,
            ""source"": ""base"",
            ""source_id"": 2339664,
            ""url"": ""https://mapy.cz/zakladni?x=14.36107759920759&y=50.04395254566995&z=17&source=base&id=2339664""
        },
        {
            ""description"": ""Cinema City Zličín"",
            ""distance"": 1232.0,
            ""imgUrl"": ""https://api.mapy.cz/poiimg/icon/62"",
            ""index"": 1,
            ""lat"": 50.05439376831055,
            ""lines"": [],
            ""lon"": 14.28779411315918,
            ""name"": ""Kino"",
            ""photo_url"": ""//d48-a.sdn.cz/d_48/c_img_H_D/kWDw40.jpeg?fl=res,{width},{height},3"",
            ""rating"": 4,
            ""review_count"": 6,
            ""source"": ""firm"",
            ""source_id"": 436222,
            ""url"": ""https://www.firmy.cz/detail/436222-cinema-city-zlicin-praha-trebonice.html""
        },
        {
            ""description"": ""Homepark Zličín"",
            ""distance"": 350.0,
            ""imgUrl"": ""https://api.mapy.cz/poiimg/icon/419"",
            ""index"": 1,
            ""lat"": 50.049190521240234,
            ""lines"": [],
            ""lon"": 14.297377586364746,
            ""name"": ""Obchod"",
            ""photo_url"": ""//d48-a.sdn.cz/d_48/c_img_G_J/QyRBDi.jpeg?fl=res,{width},{height},3"",
            ""rating"": 5,
            ""review_count"": 4,
            ""source"": ""firm"",
            ""source_id"": 13177905,
            ""time"": 531,
            ""url"": ""https://www.firmy.cz/detail/13177905-homepark-zlicin-praha-trebonice.html"",
            ""walkDistance"": 512
        },
        {
            ""description"": ""EAT> Restaurant by Sodexo"",
            ""distance"": 297.0,
            ""imgUrl"": ""https://api.mapy.cz/poiimg/icon/133"",
            ""index"": 1,
            ""lat"": 50.04782485961914,
            ""lines"": [],
            ""lon"": 14.306090354919434,
            ""name"": ""Restaurace"",
            ""photo_url"": ""//d48-a.sdn.cz/d_48/c_img_G_I/aHtEcI.jpeg?fl=res,{width},{height},3"",
            ""rating"": 3,
            ""review_count"": 3,
            ""source"": ""firm"",
            ""source_id"": 13217762,
            ""time"": 321,
            ""url"": ""https://www.firmy.cz/detail/13217762-eat-restaurant-by-sodexo-praha-stodulky.html"",
            ""walkDistance"": 357
        },
        {
            ""description"": ""Poliklinika Hůrka"",
            ""distance"": 2991.0,
            ""imgUrl"": ""https://api.mapy.cz/poiimg/icon/418"",
            ""index"": 1,
            ""lat"": 50.050296783447266,
            ""lines"": [],
            ""lon"": 14.343679428100586,
            ""name"": ""Lékař"",
            ""photo_url"": ""//d48-a.sdn.cz/d_48/c_img_gW_n/jjqLVR.jpeg?fl=res,{width},{height},3"",
            ""rating"": 5,
            ""review_count"": 5,
            ""source"": ""firm"",
            ""source_id"": 13305635,
            ""time"": 3165,
            ""url"": ""https://www.firmy.cz/detail/13305635-poliklinika-hurka-praha-stodulky.html"",
            ""walkDistance"": 3354
        },
        {
            ""description"": ""Obchodní centrum Zličín"",
            ""distance"": 392.0,
            ""imgUrl"": ""https://api.mapy.cz/poiimg/icon/198"",
            ""index"": 1,
            ""lat"": 50.05035627804245,
            ""lines"": [
                {
                    ""departure_direction"": 1,
                    ""line_id"": ""L180"",
                    ""line_label"": ""180"",
                    ""poi_id"": 15693478,
                    ""terminus"": ""Dejvická"",
                    ""type"": ""bus""
                }
            ],
            ""lon"": 14.297776493613952,
            ""name"": ""Bus MHD"",
            ""photo_url"": ""https://d18-a.sdn.cz/d_18/c_img_gV_o/CTsntw.jpeg?fl=res,300,300,3|shr,,20|jpg,90"",
            ""rating"": -1,
            ""review_count"": 0,
            ""source"": ""pubt"",
            ""source_id"": 15693478,
            ""time"": 595,
            ""url"": ""https://mapy.cz/zakladni?x=14.297776493613952&y=50.05035627804245&z=17&source=pubt&id=15693478"",
            ""walkDistance"": 583
        },
        {
            ""description"": ""Pošta Praha 515 - Česká pošta, s.p."",
            ""distance"": 501.0,
            ""imgUrl"": ""https://api.mapy.cz/poiimg/icon/122"",
            ""index"": 1,
            ""lat"": 50.046566009521484,
            ""lines"": [],
            ""lon"": 14.30856990814209,
            ""name"": ""Pošta"",
            ""photo_url"": ""//d48-a.sdn.cz/d_48/c_img_G_C/Tzcjwj.jpeg?fl=res,{width},{height},3"",
            ""rating"": 2,
            ""review_count"": 65,
            ""source"": ""firm"",
            ""source_id"": 216728,
            ""time"": 578,
            ""url"": ""https://www.firmy.cz/detail/216728-posta-praha-515-praha-stodulky.html"",
            ""walkDistance"": 635
        },
        {
            ""description"": ""Slánská"",
            ""distance"": 1893.0,
            ""imgUrl"": ""https://api.mapy.cz/poiimg/icon/155"",
            ""index"": 1,
            ""lat"": 50.0645460542,
            ""lines"": [
                {
                    ""departure_direction"": -1,
                    ""line_id"": ""L10"",
                    ""line_label"": ""10"",
                    ""poi_id"": 15303706,
                    ""terminus"": ""Sídliště Řepy"",
                    ""type"": ""tram""
                },
                {
                    ""departure_direction"": -1,
                    ""line_id"": ""L15"",
                    ""line_label"": ""15"",
                    ""poi_id"": 15303706,
                    ""terminus"": ""Sídliště Řepy"",
                    ""type"": ""tram""
                },
                {
                    ""departure_direction"": -1,
                    ""line_id"": ""L16"",
                    ""line_label"": ""16"",
                    ""poi_id"": 15303706,
                    ""terminus"": ""Sídliště Řepy"",
                    ""type"": ""tram""
                },
                {
                    ""departure_direction"": -1,
                    ""line_id"": ""L9"",
                    ""line_label"": ""9"",
                    ""poi_id"": 15303706,
                    ""terminus"": ""Sídliště Řepy"",
                    ""type"": ""tram""
                },
                {
                    ""departure_direction"": -1,
                    ""line_id"": ""L98"",
                    ""line_label"": ""98"",
                    ""poi_id"": 15303706,
                    ""terminus"": ""Sídliště Řepy"",
                    ""type"": ""tram""
                },
                {
                    ""departure_direction"": -1,
                    ""line_id"": ""L99"",
                    ""line_label"": ""99"",
                    ""poi_id"": 15303706,
                    ""terminus"": ""Sídliště Řepy"",
                    ""type"": ""tram""
                }
            ],
            ""lon"": 14.3085323654,
            ""name"": ""Tram"",
            ""photo_url"": ""https://d18-a.sdn.cz/d_18/c_img_QM_KO/D60fxG.jpeg?fl=res,300,300,3|shr,,20|jpg,90"",
            ""rating"": -1,
            ""review_count"": 0,
            ""source"": ""pubt"",
            ""source_id"": 15303706,
            ""time"": 2545,
            ""url"": ""https://mapy.cz/zakladni?x=14.3085323654&y=50.0645460542&z=17&source=pubt&id=15303706"",
            ""walkDistance"": 2647
        },
        {
            ""description"": ""Dr. Max LÉKÁRNA"",
            ""distance"": 654.0,
            ""imgUrl"": ""https://api.mapy.cz/poiimg/icon/72"",
            ""index"": 1,
            ""lat"": 50.05308532714844,
            ""lines"": [],
            ""lon"": 14.297186851501465,
            ""name"": ""Lékárna"",
            ""photo_url"": ""//d48-a.sdn.cz/d_48/c_img_QM_n/PiGln.png?fl=res,{width},{height},3"",
            ""rating"": -1,
            ""review_count"": 0,
            ""source"": ""firm"",
            ""source_id"": 13344084,
            ""time"": 1484,
            ""url"": ""https://www.firmy.cz/detail/13344084-dr-max-lekarna-praha-trebonice.html"",
            ""walkDistance"": 1547
        },
        {
            ""description"": ""Praha-Zličín"",
            ""distance"": 1817.0,
            ""imgUrl"": ""https://api.mapy.cz/poiimg/icon/169"",
            ""index"": 1,
            ""lat"": 50.0641750381,
            ""lines"": [],
            ""lon"": 14.2977438277,
            ""name"": ""Vlak"",
            ""photo_url"": ""https://d34-a.sdn.cz/d_34/c_img_gZ_b/YCED0k.jpeg?fl=res,{width},{height},3"",
            ""rating"": -1,
            ""review_count"": 0,
            ""source"": ""pubt"",
            ""source_id"": 15212589,
            ""time"": 3021,
            ""url"": ""https://mapy.cz/zakladni?x=14.2977438277&y=50.0641750381&z=17&source=pubt&id=15212589"",
            ""walkDistance"": 3216
        },
        {
            ""description"": ""MŠ Večerníček, Praha 13"",
            ""distance"": 602.0,
            ""imgUrl"": ""https://api.mapy.cz/poiimg/icon/412"",
            ""index"": 1,
            ""lat"": 50.04945373535156,
            ""lines"": [],
            ""lon"": 14.310081481933594,
            ""name"": ""Školka"",
            ""photo_url"": ""//d48-a.sdn.cz/d_48/c_img_G_C/amXBeTq.jpeg?fl=res,{width},{height},3"",
            ""rating"": 5,
            ""review_count"": 1,
            ""source"": ""firm"",
            ""source_id"": 646823,
            ""time"": 776,
            ""url"": ""https://www.firmy.cz/detail/646823-ms-vecernicek-praha-13-praha-stodulky.html"",
            ""walkDistance"": 800
        },
        {
            ""description"": ""Stodůlky"",
            ""distance"": 273.0,
            ""imgUrl"": ""https://api.mapy.cz/poiimg/icon/262"",
            ""index"": 1,
            ""lat"": 50.0465812395,
            ""lines"": [],
            ""lon"": 14.3050062943,
            ""name"": ""Metro"",
            ""photo_url"": ""https://d34-a.sdn.cz/d_34/c_B_C/ORanbQ.jpeg?fl=res,{width},{height},3"",
            ""rating"": -1,
            ""review_count"": 0,
            ""source"": ""pubt"",
            ""source_id"": 15200591,
            ""time"": 329,
            ""url"": ""https://mapy.cz/zakladni?x=14.3050062943&y=50.0465812395&z=17&source=pubt&id=15200591"",
            ""walkDistance"": 366
        },
        {
            ""description"": ""Pitland"",
            ""distance"": 1132.0,
            ""imgUrl"": ""https://api.mapy.cz/poiimg/icon/144"",
            ""index"": 1,
            ""lat"": 50.0573616027832,
            ""lines"": [],
            ""lon"": 14.295519828796387,
            ""name"": ""Sportoviště"",
            ""photo_url"": ""//d48-a.sdn.cz/d_48/c_img_gQ_N/3uCwD.jpeg?fl=res,{width},{height},3"",
            ""rating"": 5,
            ""review_count"": 3,
            ""source"": ""firm"",
            ""source_id"": 13251063,
            ""time"": 2338,
            ""url"": ""https://www.firmy.cz/detail/13251063-pitland-praha-trebonice.html"",
            ""walkDistance"": 2464
        },
        {
            ""description"": ""Bankomat ČSOB"",
            ""distance"": 311.0,
            ""imgUrl"": ""https://api.mapy.cz/poiimg/icon/28"",
            ""index"": 1,
            ""lat"": 50.04810333251953,
            ""lines"": [],
            ""lon"": 14.306290626525879,
            ""name"": ""Bankomat"",
            ""photo_url"": ""//d48-a.sdn.cz/d_48/c_img_gW_n/1h1KoY.jpeg?fl=res,{width},{height},3"",
            ""rating"": -1,
            ""review_count"": 0,
            ""source"": ""firm"",
            ""source_id"": 12776083,
            ""time"": 400,
            ""url"": ""https://www.firmy.cz/detail/12776083-bankomat-csob-praha-stodulky.html"",
            ""walkDistance"": 429
        },
        {
            ""description"": ""MŠ a ZŠ speciální Diakonie ČCE Praha"",
            ""distance"": 555.0,
            ""imgUrl"": ""https://api.mapy.cz/poiimg/icon/142"",
            ""index"": 1,
            ""lat"": 50.04896926879883,
            ""lines"": [],
            ""lon"": 14.309579849243164,
            ""name"": ""Škola"",
            ""photo_url"": ""//d48-a.sdn.cz/d_48/c_img_gQ_l/d7kSg1.jpeg?fl=res,{width},{height},3"",
            ""rating"": -1,
            ""review_count"": 0,
            ""source"": ""firm"",
            ""source_id"": 13343847,
            ""time"": 756,
            ""url"": ""https://www.firmy.cz/detail/13343847-ms-a-zs-specialni-diakonie-cce-praha-praha-stodulky.html"",
            ""walkDistance"": 778
        }
    ],
    ""poi_doctors"": {
        ""name"": ""Lékaři"",
        ""url"": ""https://www.firmy.cz/Prvni-pomoc-a-zdravotnictvi/Zdravotnicke-sluzby/Zdravotnicka-zarizeni?x=14.3026641667&y=50.0470602778&rt=adresa"",
        ""values"": [
            {
                ""description"": ""Veterinární klinika Delta MVDr.Michael Růžička, s.r.o."",
                ""distance"": 1581.0,
                ""imgUrl"": ""https://api.mapy.cz/poiimg/icon/164"",
                ""index"": 1,
                ""lat"": 50.061885833740234,
                ""lines"": [],
                ""lon"": 14.307043075561523,
                ""name"": ""Veterinář"",
                ""photo_url"": ""//d48-a.sdn.cz/d_48/c_img_G_C/ZcGBh5O.jpeg?fl=res,{width},{height},3"",
                ""rating"": 3,
                ""review_count"": 8,
                ""source"": ""firm"",
                ""source_id"": 12957413,
                ""url"": ""https://www.firmy.cz/detail/12957413-veterinarni-klinika-delta-mvdr-michael-ruzicka-praha-repy.html""
            },
            {
                ""description"": ""VETCENTRUM Duchek s.r.o."",
                ""distance"": 1870.0,
                ""imgUrl"": ""https://api.mapy.cz/poiimg/icon/164"",
                ""index"": 2,
                ""lat"": 50.057334899902344,
                ""lines"": [],
                ""lon"": 14.32378101348877,
                ""name"": ""Veterinář"",
                ""photo_url"": ""//d48-a.sdn.cz/d_48/c_img_H_D/WAEBwM0.jpeg?fl=res,{width},{height},3"",
                ""rating"": 4,
                ""review_count"": 55,
                ""source"": ""firm"",
                ""source_id"": 723312,
                ""url"": ""https://www.firmy.cz/detail/723312-vetcentrum-duchek-s-r-o-praha-stodulky.html""
            },
            {
                ""description"": ""Veterinární klinika OK-VET"",
                ""distance"": 2579.0,
                ""imgUrl"": ""https://api.mapy.cz/poiimg/icon/164"",
                ""index"": 3,
                ""lat"": 50.07121276855469,
                ""lines"": [],
                ""lon"": 14.29990291595459,
                ""name"": ""Veterinář"",
                ""photo_url"": ""//d48-a.sdn.cz/d_48/c_img_H_D/JYniBs.jpeg?fl=res,{width},{height},3"",
                ""rating"": 5,
                ""review_count"": 14,
                ""source"": ""firm"",
                ""source_id"": 439625,
                ""url"": ""https://www.firmy.cz/detail/439625-veterinarni-klinika-ok-vet-praha-repy.html""
            },
            {
                ""description"": ""Veterinární klinika Na Hůrce"",
                ""distance"": 2661.0,
                ""imgUrl"": ""https://api.mapy.cz/poiimg/icon/164"",
                ""index"": 4,
                ""lat"": 50.05284118652344,
                ""lines"": [],
                ""lon"": 14.338454246520996,
                ""name"": ""Veterinář"",
                ""photo_url"": ""//d48-a.sdn.cz/d_48/c_img_E_C/nkyDWa.jpeg?fl=res,{width},{height},3"",
                ""rating"": 5,
                ""review_count"": 4,
                ""source"": ""firm"",
                ""source_id"": 473619,
                ""url"": ""https://www.firmy.cz/detail/473619-veterinarni-klinika-na-hurce-praha-stodulky.html""
            },
            {
                ""description"": ""MVDr. Marie Lietavcová"",
                ""distance"": 2661.0,
                ""imgUrl"": ""https://api.mapy.cz/poiimg/icon/164"",
                ""index"": 5,
                ""lat"": 50.05284118652344,
                ""lines"": [],
                ""lon"": 14.338454246520996,
                ""name"": ""Veterinář"",
                ""photo_url"": ""//d48-a.sdn.cz/d_48/c_img_H_D/kfNkBI.jpeg?fl=res,{width},{height},3"",
                ""rating"": 5,
                ""review_count"": 4,
                ""source"": ""firm"",
                ""source_id"": 2139557,
                ""url"": ""https://www.firmy.cz/detail/2139557-mvdr-marie-lietavcova-praha-stodulky.html""
            }
        ]
    },
    ""poi_grocery"": {
        ""name"": ""Potraviny"",
        ""url"": ""https://www.firmy.cz/Obchody-a-obchudky/Prodejci-potravin?x=14.3026641667&y=50.0470602778&rt=adresa"",
        ""values"": [
            {
                ""description"": ""Potraviny Svitákova 7"",
                ""distance"": 115.0,
                ""imgUrl"": ""https://api.mapy.cz/poiimg/icon/423"",
                ""index"": 1,
                ""lat"": 50.04705047607422,
                ""lines"": [],
                ""lon"": 14.302352905273438,
                ""name"": ""Večerka"",
                ""photo_url"": ""//d48-a.sdn.cz/d_48/c_img_gR_m/ZqlLTx.jpeg?fl=res,{width},{height},3"",
                ""rating"": 4,
                ""review_count"": 1,
                ""source"": ""firm"",
                ""source_id"": 13163774,
                ""url"": ""https://www.firmy.cz/detail/13163774-potraviny-svitakova-7-praha-stodulky.html""
            },
            {
                ""description"": ""Homepark Zličín"",
                ""distance"": 350.0,
                ""imgUrl"": ""https://api.mapy.cz/poiimg/icon/419"",
                ""index"": 1,
                ""lat"": 50.049190521240234,
                ""lines"": [],
                ""lon"": 14.297377586364746,
                ""name"": ""Obchod"",
                ""photo_url"": ""//d48-a.sdn.cz/d_48/c_img_G_J/QyRBDi.jpeg?fl=res,{width},{height},3"",
                ""rating"": 5,
                ""review_count"": 4,
                ""source"": ""firm"",
                ""source_id"": 13177905,
                ""time"": 531,
                ""url"": ""https://www.firmy.cz/detail/13177905-homepark-zlicin-praha-trebonice.html"",
                ""walkDistance"": 512
            },
            {
                ""description"": ""Potraviny Vlachova"",
                ""distance"": 511.0,
                ""imgUrl"": ""https://api.mapy.cz/poiimg/icon/423"",
                ""index"": 2,
                ""lat"": 50.04745101928711,
                ""lines"": [],
                ""lon"": 14.309041976928711,
                ""name"": ""Večerka"",
                ""photo_url"": ""//d48-a.sdn.cz/d_48/c_img_gU_m/q3hJnd.jpeg?fl=res,{width},{height},3"",
                ""rating"": 5,
                ""review_count"": 1,
                ""source"": ""firm"",
                ""source_id"": 12917471,
                ""url"": ""https://www.firmy.cz/detail/12917471-potraviny-vlachova-praha-stodulky.html""
            },
            {
                ""description"": ""Tesco Extra"",
                ""distance"": 534.0,
                ""imgUrl"": ""https://api.mapy.cz/poiimg/icon/101"",
                ""index"": 2,
                ""lat"": 50.04939270019531,
                ""lines"": [],
                ""lon"": 14.294756889343262,
                ""name"": ""Obchod"",
                ""photo_url"": ""//d48-a.sdn.cz/d_48/c_img_G_C/268jvg.jpeg?fl=res,{width},{height},3"",
                ""rating"": 4,
                ""review_count"": 8,
                ""source"": ""firm"",
                ""source_id"": 733910,
                ""url"": ""https://www.firmy.cz/detail/733910-tesco-extra-praha-trebonice.html""
            },
            {
                ""description"": ""Globus Hypermarket"",
                ""distance"": 618.0,
                ""imgUrl"": ""https://api.mapy.cz/poiimg/icon/101"",
                ""index"": 3,
                ""lat"": 50.0528450012207,
                ""lines"": [],
                ""lon"": 14.297562599182129,
                ""name"": ""Obchod"",
                ""photo_url"": ""//d48-a.sdn.cz/d_48/c_img_E_E/U3BBbG.jpeg?fl=res,{width},{height},3"",
                ""rating"": 5,
                ""review_count"": 21,
                ""source"": ""firm"",
                ""source_id"": 448342,
                ""url"": ""https://www.firmy.cz/detail/448342-globus-hypermarket-praha-trebonice.html""
            }
        ]
    },
    ""poi_leisure_time"": {
        ""name"": ""Volný čas"",
        ""url"": ""https://mapy.cz/zakladni?x=14.3026641667&y=50.0470602778&z=16&q=volny%20%C4%8Das"",
        ""values"": [
            {
                ""description"": ""Pitland"",
                ""distance"": 1132.0,
                ""imgUrl"": ""https://api.mapy.cz/poiimg/icon/144"",
                ""index"": 1,
                ""lat"": 50.0573616027832,
                ""lines"": [],
                ""lon"": 14.295519828796387,
                ""name"": ""Sportoviště"",
                ""photo_url"": ""//d48-a.sdn.cz/d_48/c_img_gQ_N/3uCwD.jpeg?fl=res,{width},{height},3"",
                ""rating"": 5,
                ""review_count"": 3,
                ""source"": ""firm"",
                ""source_id"": 13251063,
                ""time"": 2338,
                ""url"": ""https://www.firmy.cz/detail/13251063-pitland-praha-trebonice.html"",
                ""walkDistance"": 2464
            },
            {
                ""description"": ""Metropole na ledě"",
                ""distance"": 1178.0,
                ""imgUrl"": ""https://api.mapy.cz/poiimg/icon/144"",
                ""index"": 2,
                ""lat"": 50.0543212890625,
                ""lines"": [],
                ""lon"": 14.2886323928833,
                ""name"": ""Sportoviště"",
                ""photo_url"": ""//d48-a.sdn.cz/d_48/c_img_QJ_i/VWWCvx.jpeg?fl=res,{width},{height},3"",
                ""rating"": 5,
                ""review_count"": 7,
                ""source"": ""firm"",
                ""source_id"": 13338939,
                ""url"": ""https://www.firmy.cz/detail/13338939-metropole-na-lede-praha-trebonice.html""
            },
            {
                ""description"": ""Cinema City Zličín"",
                ""distance"": 1232.0,
                ""imgUrl"": ""https://api.mapy.cz/poiimg/icon/62"",
                ""index"": 1,
                ""lat"": 50.05439376831055,
                ""lines"": [],
                ""lon"": 14.28779411315918,
                ""name"": ""Kino"",
                ""photo_url"": ""//d48-a.sdn.cz/d_48/c_img_H_D/kWDw40.jpeg?fl=res,{width},{height},3"",
                ""rating"": 4,
                ""review_count"": 6,
                ""source"": ""firm"",
                ""source_id"": 436222,
                ""url"": ""https://www.firmy.cz/detail/436222-cinema-city-zlicin-praha-trebonice.html""
            },
            {
                ""description"": ""Best Bowling"",
                ""distance"": 1273.0,
                ""imgUrl"": ""https://api.mapy.cz/poiimg/icon/31"",
                ""index"": 3,
                ""lat"": 50.053749084472656,
                ""lines"": [],
                ""lon"": 14.286476135253906,
                ""name"": ""Sportoviště"",
                ""photo_url"": ""//d48-a.sdn.cz/d_48/c_img_H_D/InTw5A.jpeg?fl=res,{width},{height},3"",
                ""rating"": 5,
                ""review_count"": 2,
                ""source"": ""firm"",
                ""source_id"": 580770,
                ""url"": ""https://www.firmy.cz/detail/580770-best-bowling-praha-trebonice.html""
            },
            {
                ""description"": ""Divadelní studio Bubec"",
                ""distance"": 1377.0,
                ""imgUrl"": ""https://api.mapy.cz/poiimg/icon/39"",
                ""index"": 1,
                ""lat"": 50.036521911621094,
                ""lines"": [],
                ""lon"": 14.308954238891602,
                ""name"": ""Divadlo"",
                ""photo_url"": ""//d48-a.sdn.cz/d_48/c_img_gX_n/2fdLPP.jpeg?fl=res,{width},{height},3"",
                ""rating"": 2,
                ""review_count"": 1,
                ""source"": ""firm"",
                ""source_id"": 13047862,
                ""url"": ""https://www.firmy.cz/detail/13047862-divadelni-studio-bubec-praha-reporyje.html""
            }
        ]
    },
    ""poi_restaurant"": {
        ""name"": ""Restaurace"",
        ""url"": ""https://www.firmy.cz/Restauracni-a-pohostinske-sluzby?x=14.3026641667&y=50.0470602778&rt=adresa"",
        ""values"": [
            {
                ""description"": ""La Zmrzka"",
                ""distance"": 237.0,
                ""imgUrl"": ""https://api.mapy.cz/poiimg/icon/38"",
                ""index"": 1,
                ""lat"": 50.046173095703125,
                ""lines"": [],
                ""lon"": 14.303495407104492,
                ""name"": ""Cukrárna"",
                ""photo_url"": ""//d48-a.sdn.cz/d_48/c_img_H_D/FbpCEGd.jpeg?fl=res,{width},{height},3"",
                ""rating"": 5,
                ""review_count"": 7,
                ""source"": ""firm"",
                ""source_id"": 13065320,
                ""url"": ""https://www.firmy.cz/detail/13065320-la-zmrzka-praha-stodulky.html""
            },
            {
                ""description"": ""EAT> Restaurant by Sodexo"",
                ""distance"": 297.0,
                ""imgUrl"": ""https://api.mapy.cz/poiimg/icon/133"",
                ""index"": 1,
                ""lat"": 50.04782485961914,
                ""lines"": [],
                ""lon"": 14.306090354919434,
                ""name"": ""Restaurace"",
                ""photo_url"": ""//d48-a.sdn.cz/d_48/c_img_G_I/aHtEcI.jpeg?fl=res,{width},{height},3"",
                ""rating"": 3,
                ""review_count"": 3,
                ""source"": ""firm"",
                ""source_id"": 13217762,
                ""time"": 321,
                ""url"": ""https://www.firmy.cz/detail/13217762-eat-restaurant-by-sodexo-praha-stodulky.html"",
                ""walkDistance"": 357
            },
            {
                ""description"": ""Bartga Bistro & Italian Pizza"",
                ""distance"": 311.0,
                ""imgUrl"": ""https://api.mapy.cz/poiimg/icon/133"",
                ""index"": 2,
                ""lat"": 50.04810333251953,
                ""lines"": [],
                ""lon"": 14.306290626525879,
                ""name"": ""Restaurace"",
                ""photo_url"": ""//d48-a.sdn.cz/d_48/c_img_G_C/KScBw5Y.jpeg?fl=res,{width},{height},3"",
                ""rating"": 5,
                ""review_count"": 1,
                ""source"": ""firm"",
                ""source_id"": 13001247,
                ""url"": ""https://www.firmy.cz/detail/13001247-bartga-bistro-italian-pizza-praha-stodulky.html""
            },
            {
                ""description"": ""Kolkovna Stodůlky"",
                ""distance"": 324.0,
                ""imgUrl"": ""https://api.mapy.cz/poiimg/icon/133"",
                ""index"": 3,
                ""lat"": 50.047000885009766,
                ""lines"": [],
                ""lon"": 14.30616569519043,
                ""name"": ""Restaurace"",
                ""photo_url"": ""//d48-a.sdn.cz/d_48/c_img_H_D/1DZBoPG.jpeg?fl=res,{width},{height},3"",
                ""rating"": 4,
                ""review_count"": 4,
                ""source"": ""firm"",
                ""source_id"": 12733983,
                ""url"": ""https://www.firmy.cz/detail/12733983-kolkovna-stodulky-praha-stodulky.html""
            },
            {
                ""description"": ""Restaurace Puzzlesalads"",
                ""distance"": 332.0,
                ""imgUrl"": ""https://api.mapy.cz/poiimg/icon/133"",
                ""index"": 4,
                ""lat"": 50.047332763671875,
                ""lines"": [],
                ""lon"": 14.306455612182617,
                ""name"": ""Restaurace"",
                ""photo_url"": ""//d48-a.sdn.cz/d_48/c_img_gU_m/1g6LlT.jpeg?fl=res,{width},{height},3"",
                ""rating"": -1,
                ""review_count"": 0,
                ""source"": ""firm"",
                ""source_id"": 12974261,
                ""url"": ""https://www.firmy.cz/detail/12974261-restaurace-puzzlesalads-praha-stodulky.html""
            }
        ]
    },
    ""poi_school_kindergarten"": {
        ""name"": ""Školy a školky"",
        ""url"": ""https://www.firmy.cz/Instituce-a-urady/Vzdelavaci-instituce?x=14.3026641667&y=50.0470602778&rt=adresa"",
        ""values"": [
            {
                ""description"": ""MŠ a ZŠ speciální Diakonie ČCE Praha"",
                ""distance"": 555.0,
                ""imgUrl"": ""https://api.mapy.cz/poiimg/icon/142"",
                ""index"": 1,
                ""lat"": 50.04896926879883,
                ""lines"": [],
                ""lon"": 14.309579849243164,
                ""name"": ""Škola"",
                ""photo_url"": ""//d48-a.sdn.cz/d_48/c_img_gQ_l/d7kSg1.jpeg?fl=res,{width},{height},3"",
                ""rating"": -1,
                ""review_count"": 0,
                ""source"": ""firm"",
                ""source_id"": 13343847,
                ""time"": 756,
                ""url"": ""https://www.firmy.cz/detail/13343847-ms-a-zs-specialni-diakonie-cce-praha-praha-stodulky.html"",
                ""walkDistance"": 778
            },
            {
                ""description"": ""MŠ Večerníček, Praha 13"",
                ""distance"": 602.0,
                ""imgUrl"": ""https://api.mapy.cz/poiimg/icon/412"",
                ""index"": 1,
                ""lat"": 50.04945373535156,
                ""lines"": [],
                ""lon"": 14.310081481933594,
                ""name"": ""Školka"",
                ""photo_url"": ""//d48-a.sdn.cz/d_48/c_img_G_C/amXBeTq.jpeg?fl=res,{width},{height},3"",
                ""rating"": 5,
                ""review_count"": 1,
                ""source"": ""firm"",
                ""source_id"": 646823,
                ""time"": 776,
                ""url"": ""https://www.firmy.cz/detail/646823-ms-vecernicek-praha-13-praha-stodulky.html"",
                ""walkDistance"": 800
            },
            {
                ""description"": ""Bilingvální mateřská škola pro sluchově postižené s.r.o."",
                ""distance"": 744.0,
                ""imgUrl"": ""https://api.mapy.cz/poiimg/icon/412"",
                ""index"": 2,
                ""lat"": 50.04633712768555,
                ""lines"": [],
                ""lon"": 14.312005996704102,
                ""name"": ""Školka"",
                ""photo_url"": ""//d48-a.sdn.cz/d_48/c_img_G_C/JwoBeY0.jpeg?fl=res,{width},{height},3"",
                ""rating"": -1,
                ""review_count"": 0,
                ""source"": ""firm"",
                ""source_id"": 608412,
                ""url"": ""https://www.firmy.cz/detail/608412-bilingvalni-materska-skola-pro-sluchove-postizene-s-r-o-praha-stodulky.html""
            },
            {
                ""description"": ""Rodinné centrum Hvězdička, s.r.o."",
                ""distance"": 759.0,
                ""imgUrl"": ""https://api.mapy.cz/poiimg/icon/412"",
                ""index"": 3,
                ""lat"": 50.0488166809082,
                ""lines"": [],
                ""lon"": 14.312500953674316,
                ""name"": ""Školka"",
                ""photo_url"": ""//d48-a.sdn.cz/d_48/c_img_G_C/5CuBeT2.jpeg?fl=res,{width},{height},3"",
                ""rating"": -1,
                ""review_count"": 0,
                ""source"": ""firm"",
                ""source_id"": 12771309,
                ""url"": ""https://www.firmy.cz/detail/12771309-rodinne-centrum-hvezdicka-praha-stodulky.html""
            },
            {
                ""description"": ""ZUŠ Praha 5 - Stodůlky, K Brance"",
                ""distance"": 771.0,
                ""imgUrl"": ""https://api.mapy.cz/poiimg/icon/142"",
                ""index"": 2,
                ""lat"": 50.04878234863281,
                ""lines"": [],
                ""lon"": 14.3126859664917,
                ""name"": ""Škola"",
                ""photo_url"": ""//d48-a.sdn.cz/d_48/c_img_G_C/PDrBeT3.jpeg?fl=res,{width},{height},3"",
                ""rating"": -1,
                ""review_count"": 0,
                ""source"": ""firm"",
                ""source_id"": 427116,
                ""url"": ""https://www.firmy.cz/detail/427116-zus-praha-5-stodulky-k-brance-praha-stodulky.html""
            }
        ]
    },
    ""poi_transport"": {
        ""name"": ""Doprava"",
        ""url"": ""https://mapy.cz/zakladni?x=14.3026641667&y=50.0470602778&z=16&q=zast%C3%A1vky"",
        ""values"": [
            {
                ""description"": ""Stodůlky"",
                ""distance"": 273.0,
                ""imgUrl"": ""https://api.mapy.cz/poiimg/icon/262"",
                ""index"": 1,
                ""lat"": 50.0465812395,
                ""lines"": [],
                ""lon"": 14.3050062943,
                ""name"": ""Metro"",
                ""photo_url"": ""https://d34-a.sdn.cz/d_34/c_B_C/ORanbQ.jpeg?fl=res,{width},{height},3"",
                ""rating"": -1,
                ""review_count"": 0,
                ""source"": ""pubt"",
                ""source_id"": 15200591,
                ""time"": 329,
                ""url"": ""https://mapy.cz/zakladni?x=14.3050062943&y=50.0465812395&z=17&source=pubt&id=15200591"",
                ""walkDistance"": 366
            },
            {
                ""description"": ""Stodůlky"",
                ""distance"": 351.0,
                ""imgUrl"": ""https://api.mapy.cz/poiimg/icon/262"",
                ""index"": 2,
                ""lat"": 50.0466831,
                ""lines"": [],
                ""lon"": 14.3063769,
                ""name"": ""Metro"",
                ""photo_url"": ""https://d34-a.sdn.cz/d_34/c_img_G_G/Np3jQX.jpeg?fl=res,{width},{height},3"",
                ""rating"": -1,
                ""review_count"": 0,
                ""source"": ""pubt"",
                ""source_id"": 15709352,
                ""url"": ""https://mapy.cz/zakladni?x=14.3063769&y=50.0466831&z=17&source=pubt&id=15709352""
            },
            {
                ""description"": ""Obchodní centrum Zličín"",
                ""distance"": 392.0,
                ""imgUrl"": ""https://api.mapy.cz/poiimg/icon/198"",
                ""index"": 1,
                ""lat"": 50.05035627804245,
                ""lines"": [
                    {
                        ""departure_direction"": 1,
                        ""line_id"": ""L180"",
                        ""line_label"": ""180"",
                        ""poi_id"": 15693478,
                        ""terminus"": ""Dejvická"",
                        ""type"": ""bus""
                    }
                ],
                ""lon"": 14.297776493613952,
                ""name"": ""Bus MHD"",
                ""photo_url"": ""https://d18-a.sdn.cz/d_18/c_img_gV_o/CTsntw.jpeg?fl=res,300,300,3|shr,,20|jpg,90"",
                ""rating"": -1,
                ""review_count"": 0,
                ""source"": ""pubt"",
                ""source_id"": 15693478,
                ""time"": 595,
                ""url"": ""https://mapy.cz/zakladni?x=14.297776493613952&y=50.05035627804245&z=17&source=pubt&id=15693478"",
                ""walkDistance"": 583
            },
            {
                ""description"": ""Obchodní centrum Zličín"",
                ""distance"": 412.0,
                ""imgUrl"": ""https://api.mapy.cz/poiimg/icon/198"",
                ""index"": 2,
                ""lat"": 50.04955387614435,
                ""lines"": [],
                ""lon"": 14.296669020118905,
                ""name"": ""Bus MHD"",
                ""photo_url"": ""https://d18-a.sdn.cz/d_18/c_img_gZ_n/fNnnxW.jpeg?fl=res,300,300,3|shr,,20|jpg,90"",
                ""rating"": -1,
                ""review_count"": 0,
                ""source"": ""pubt"",
                ""source_id"": 15693539,
                ""url"": ""https://mapy.cz/zakladni?x=14.296669020118905&y=50.04955387614435&z=17&source=pubt&id=15693539""
            },
            {
                ""description"": ""Stodůlky"",
                ""distance"": 428.0,
                ""imgUrl"": ""https://api.mapy.cz/poiimg/icon/262"",
                ""index"": 3,
                ""lat"": 50.0466394883,
                ""lines"": [],
                ""lon"": 14.3075099294,
                ""name"": ""Metro"",
                ""photo_url"": ""https://d34-a.sdn.cz/d_34/c_E_B/Ljzb2.jpeg?fl=res,{width},{height},3"",
                ""rating"": -1,
                ""review_count"": 0,
                ""source"": ""pubt"",
                ""source_id"": 15200595,
                ""url"": ""https://mapy.cz/zakladni?x=14.3075099294&y=50.0466394883&z=17&source=pubt&id=15200595""
            }
        ]
    },
    ""price_czk"": {
        ""name"": ""Celková cena"",
        ""unit"": ""za měsíc"",
        ""value"": ""30 000"",
        ""value_raw"": 30000
    },
    ""rus"": false,
    ""seo"": {
        ""category_main_cb"": 1,
        ""category_sub_cb"": 6,
        ""category_type_cb"": 2,
        ""locality"": ""praha-stodulky-svitakova""
    },
    ""text"": {
        ""name"": ""Popis"",
        ""value"": ""Nabídka velice slunného bytu 3+kk s prostornými terasami, který se nachází ve 4. np. bytového domu, v ulici Svitákova. Celková výměra bytu činí 81,4 m2 + 3 terasy přístupné ze všech pokojů o rozloze 86 m2. K této jednotce náleží také garážové stání a sklep. Obývací pokoj s kuchyňským koutem, který nabízí základní spotřebiče jako je myčka, lednice s mrazákem, trouba, mikrovlnka. Koupelna s vanou, samostatné WC a komora. \r\n\r\nBytový dům se nachází ve velmi příjemné lokalitě - Britská čtvrť, s kompletní občanskou vybaveností a vedle stanice metra Stodůlky. Cena nájemného vč. garážového stání je 30 000,- Kč + energie 6 580 Kč. \r\n\r\nByt je připravený k nastěhování okamžitě. Prohlídky jsou možné po dohodě, neváhejte se ozvat.""
    }
}
";

        [Test]
        public void Test1()
        {
            var responseObject = JsonSerializer.Deserialize<SRealityEstateDetails>(detailsResponse);

            Assert.IsNotEmpty(responseObject.text.value.GetString());
            Assert.IsNotEmpty(responseObject._embedded.images);
            Assert.IsTrue(responseObject.HasElevator);
            Assert.AreEqual("4", responseObject.Floor);
        }

        [Test]
        public void TranslatorTest()
        {
            var translatedContract = @"[{""translations"":[{""text"":""Предлагаем в аренду красивую квартиру 3+1 площадью 109 м2 на 1 этаже исторического дома в Виноградах, на улице Италии. Также подходит для соседей по комнате. <бр / > Планировка квартиры состоит из 3 отдельных комнат, просторной прихожей, кухни с полностью оборудованной кухней, 2 ванных комнат, одна с душем, а другая с ванной. Кухня и одна из комнат выходят в тихий внутренний двор. В апартаментах с роскошными деревянными полами. Оба номера полностью меблированы. <бр />Дом расположен в дворянском районе Винограды рядом с театром Винограды. В окрестностях есть полные гражданские удобства, услуги, рестораны, магазины, спортивные сооружения. Хорошо доступное место на метро и трамвае (Náměstí Míru и музей). Винограды окружены несколькими тщательно ухоженными парками. Ригровы сады находятся почти у дома, также можно прогуляться до Гребовки. Пешая доступность до центра занимает всего несколько минут. <br / > Плата за электроэнергию и коммунальные услуги добавляется к стоимости аренды."",""to"":""ru""}]}]";

            TranslationResult[] deserializedOutput = JsonSerializer.Deserialize<TranslationResult[]>(translatedContract);

            Assert.IsNotEmpty(deserializedOutput);
        }
        /// <summary>
        /// The C# classes that represents the JSON returned by the Translator Text API.
        /// </summary>
        public class TranslationResult
        {
            public DetectedLanguage DetectedLanguage { get; set; }
            public TextResult SourceText { get; set; }
            [JsonPropertyName("translations")]
            public Translation[] Translations { get; set; }
        }

        public class DetectedLanguage
        {
            public string Language { get; set; }
            public float Score { get; set; }
        }

        public class TextResult
        {
            public string Text { get; set; }
            public string Script { get; set; }
        }

        public class Translation
        {
            [JsonPropertyName("text")]
            public string Text { get; set; }
            public TextResult Transliteration { get; set; }
            public string To { get; set; }
            public Alignment Alignment { get; set; }
            public SentenceLength SentLen { get; set; }
        }

        public class Alignment
        {
            public string Proj { get; set; }
        }

        public class SentenceLength
        {
            public int[] SrcSentLen { get; set; }
            public int[] TransSentLen { get; set; }
        }

    }
}