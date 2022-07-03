namespace Eagles.LMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class intiatsss : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AboutUs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.String(),
                        TitleArabic = c.String(),
                        TitleEnglish = c.String(),
                        DescriptionArabic = c.String(),
                        DescriptionEnglish = c.String(),
                        YouTubeFrame = c.String(nullable: false),
                        WHoWeDescriptionArabic = c.String(),
                        WhoWeDescriptionEnglish = c.String(),
                        ImageWhoWe = c.String(),
                        ProfileDescriptionArabic = c.String(),
                        ProfileDescriptionEnglish = c.String(),
                        MessageTapDescriptionArabic = c.String(),
                        MessageTapDescriptionEnglish = c.String(),
                        Order = c.Int(nullable: false),
                        EditeTime = c.DateTime(nullable: false),
                        UserEditId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Agenda",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MainImage = c.String(),
                        TitleArabic = c.String(),
                        TitleEnglish = c.String(),
                        DescriptionArabic = c.String(),
                        DescriptionEnglish = c.String(),
                        Status = c.Int(nullable: false),
                        Order = c.Int(nullable: false),
                        AgendaDate = c.DateTime(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        UserCreateId = c.Int(nullable: false),
                        ShortDescriptionArabic = c.String(),
                        ShortDescriptionEnglish = c.String(),
                        EditeTime = c.DateTime(nullable: false),
                        UserEditId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AgendaImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Path = c.String(),
                        AgendaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Agenda", t => t.AgendaId, cascadeDelete: true)
                .Index(t => t.AgendaId);
            
            CreateTable(
                "dbo.Albums",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TitleArabic = c.String(),
                        TitleEnglish = c.String(),
                        Image = c.String(),
                        Order = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        UserCreateId = c.Int(nullable: false),
                        EditeTime = c.DateTime(nullable: false),
                        UserEditId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Galaries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.String(),
                        TitleArabic = c.String(),
                        TitleEnglish = c.String(),
                        Order = c.Int(nullable: false),
                        IsImage = c.Boolean(nullable: false),
                        VideoIframe = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UserCreateId = c.Int(nullable: false),
                        EditeTime = c.DateTime(nullable: false),
                        UserEditId = c.Int(nullable: false),
                        AlbumId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Albums", t => t.AlbumId)
                .Index(t => t.AlbumId);
            
            CreateTable(
                "dbo.BookingInServices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookingServiceId = c.Int(nullable: false),
                        BookingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bookings", t => t.BookingId, cascadeDelete: true)
                .ForeignKey("dbo.BookingServices", t => t.BookingServiceId, cascadeDelete: true)
                .Index(t => t.BookingServiceId)
                .Index(t => t.BookingId);
            
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(),
                        CompanyEmail = c.String(),
                        DataServicees = c.String(),
                        SartTime = c.String(),
                        EndTime = c.String(),
                        Origin = c.String(),
                        Message = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BookingServices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Citizens",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TitleArabic = c.String(),
                        TitleEnglish = c.String(),
                        DescriptionArabic = c.String(),
                        DescriptionEnglish = c.String(),
                        Image = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UserCreateId = c.Int(nullable: false),
                        EditeTime = c.DateTime(nullable: false),
                        UserEditId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.String(),
                        NameArabic = c.String(),
                        NameEnglish = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UserCreateId = c.Int(nullable: false),
                        EditeTime = c.DateTime(nullable: false),
                        UserEditId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ConatctRequist",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        JobTitle = c.String(),
                        Organisation = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        Country = c.String(),
                        Subject = c.String(),
                        Message = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContactUs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Phone = c.String(nullable: false),
                        AddressArabic = c.String(),
                        AddressEnglish = c.String(),
                        Email = c.String(nullable: false),
                        Logo = c.String(),
                        CompanyTitleArabic = c.String(),
                        CompanyTitleEnglish = c.String(),
                        CitizenEmail = c.String(),
                        EditeTime = c.DateTime(nullable: false),
                        UserEditId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.String(),
                        NameArabic = c.String(),
                        NameEnglish = c.String(),
                        MessageArabic = c.String(),
                        MessageEnglish = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UserCreateId = c.Int(nullable: false),
                        EditeTime = c.DateTime(nullable: false),
                        UserEditId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.String(),
                        TitleArabic = c.String(),
                        TitleEnglish = c.String(),
                        DescriptionArabic = c.String(),
                        DescriptionEnglish = c.String(),
                        EditeTime = c.DateTime(nullable: false),
                        UserEditId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Facilities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.String(),
                        TitleArabic = c.String(),
                        TitleEnglish = c.String(),
                        ShortDiscriptionArabic = c.String(),
                        ShortDiscriptionEnglish = c.String(),
                        DiscriptionArabic = c.String(),
                        DiscriptionEnglish = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UserCreateId = c.Int(nullable: false),
                        EditeTime = c.DateTime(nullable: false),
                        UserEditId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Filmed",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MainImage = c.String(),
                        VideoIframe = c.String(),
                        TitleArabic = c.String(),
                        TitleEnglish = c.String(),
                        TypeArabic = c.String(),
                        TypeEnglish = c.String(),
                        ViewsNumber = c.String(),
                        DescriptionArabic = c.String(),
                        DescriptionEnglish = c.String(),
                        LanguageArabic = c.String(),
                        LanguageEnglish = c.String(),
                        SubtitleArabic = c.String(),
                        SubtitleEnglish = c.String(),
                        AudioLanguageArabic = c.String(),
                        AudioLanguageEnglish = c.String(),
                        GenreArabic = c.String(),
                        GenreEnglish = c.String(),
                        RunTime = c.String(),
                        ReleaseDate = c.DateTime(nullable: false),
                        ShowInHomePage = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        Order = c.Int(nullable: false),
                        UserCreateId = c.Int(nullable: false),
                        EditeTime = c.DateTime(nullable: false),
                        UserEditId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FilmedImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Path = c.String(),
                        FilmedId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Filmed", t => t.FilmedId, cascadeDelete: true)
                .Index(t => t.FilmedId);
            
            CreateTable(
                "dbo.GroupPriviages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PrivilageId = c.Int(nullable: false),
                        GroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.Privilages", t => t.PrivilageId, cascadeDelete: true)
                .Index(t => t.PrivilageId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Mobile = c.String(nullable: false),
                        EmailAddress = c.String(),
                        PasswordHash = c.Binary(nullable: false),
                        PasswordSalt = c.Binary(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        UserTybe = c.Int(nullable: false),
                        AccountState = c.Int(nullable: false),
                        FireBaseToken = c.String(),
                        FullName = c.String(nullable: false),
                        latitude = c.String(),
                        altitude = c.String(),
                        Image = c.String(),
                        GroupId = c.Int(),
                        IsDeleted = c.Boolean(nullable: false),
                        OTP = c.String(),
                        OTP_Provider = c.String(),
                        OTPTIME = c.DateTime(nullable: false),
                        LoginDate = c.DateTime(),
                        LogoutDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.GroupId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.Privilages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MenueName = c.String(),
                        ParentId = c.Int(),
                        IsRoute = c.Boolean(nullable: false),
                        OrderId = c.Int(nullable: false),
                        ShowInMenue = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PrivilageRoutes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Area = c.String(),
                        Controller = c.String(),
                        Action = c.String(),
                        Url = c.String(),
                        PrivilageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Privilages", t => t.PrivilageId, cascadeDelete: true)
                .Index(t => t.PrivilageId);
            
            CreateTable(
                "dbo.LocationImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Path = c.String(),
                        LocationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Locations", t => t.LocationId, cascadeDelete: true)
                .Index(t => t.LocationId);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IconImage = c.String(),
                        MainImage = c.String(),
                        TitleArabic = c.String(),
                        TitleEnglish = c.String(),
                        DescriptionArabic = c.String(),
                        DescriptionEnglish = c.String(),
                        LocationVideo = c.String(),
                        Status = c.Int(nullable: false),
                        AddressArabic = c.String(),
                        AddressEnglish = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UserCreateId = c.Int(nullable: false),
                        ShortDescriptionArabic = c.String(),
                        ShortDescriptionEnglish = c.String(),
                        EditeTime = c.DateTime(nullable: false),
                        UserEditId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.logs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Action = c.String(),
                        ActionTime = c.DateTime(nullable: false),
                        TableName = c.String(),
                        EntityId = c.Int(),
                        LoginDate = c.DateTime(),
                        LogoutDate = c.DateTime(),
                        ActionTitle = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.NewImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Path = c.String(),
                        NewId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.News", t => t.NewId, cascadeDelete: true)
                .Index(t => t.NewId);
            
            CreateTable(
                "dbo.News",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MainImage = c.String(),
                        TitleArabic = c.String(),
                        TitleEnglish = c.String(),
                        DescriptionArabic = c.String(),
                        DescriptionEnglish = c.String(),
                        NewsDate = c.DateTime(nullable: false),
                        ShowInHomePage = c.Boolean(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        Order = c.Int(nullable: false),
                        NotsEnglish = c.String(),
                        NotsArabic = c.String(),
                        UserCreateId = c.Int(nullable: false),
                        EditeTime = c.DateTime(nullable: false),
                        UserEditId = c.Int(nullable: false),
                        ShortDescriptionArabic = c.String(),
                        ShortDescriptionEnglish = c.String(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Procedures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.String(),
                        TitleArabic = c.String(),
                        TitleEnglish = c.String(),
                        DescriptionArabic = c.String(),
                        DescriptionEnglish = c.String(),
                        EditeTime = c.DateTime(nullable: false),
                        UserEditId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RelatedWebSites",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.String(),
                        TitleArabic = c.String(),
                        TitleEnglish = c.String(),
                        DescriptionArabic = c.String(),
                        DescriptionEnglish = c.String(),
                        Link = c.String(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        UserCreateId = c.Int(nullable: false),
                        EditeTime = c.DateTime(nullable: false),
                        UserEditId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ServiceImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Path = c.String(),
                        ServiceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Services", t => t.ServiceId, cascadeDelete: true)
                .Index(t => t.ServiceId);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MainImage = c.String(),
                        TitleArabic = c.String(),
                        TitleEnglish = c.String(),
                        Status = c.Int(nullable: false),
                        DescriptionArabic = c.String(),
                        DescriptionEnglish = c.String(),
                        ShortDescriptionArabic = c.String(),
                        ShortDescriptionEnglish = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UserCreateId = c.Int(nullable: false),
                        EditeTime = c.DateTime(nullable: false),
                        UserEditId = c.Int(nullable: false),
                        ServicesIframe = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EditeTime = c.DateTime(nullable: false),
                        UserEditId = c.Int(nullable: false),
                        ShowAboutUs = c.Boolean(nullable: false),
                        ShowAgenda = c.Boolean(nullable: false),
                        ShowCitizen = c.Boolean(nullable: false),
                        ShowContactUs = c.Boolean(nullable: false),
                        ShowGalaryPhotos = c.Boolean(nullable: false),
                        ShowGalarVideos = c.Boolean(nullable: false),
                        ShowLocations = c.Boolean(nullable: false),
                        ShowNews = c.Boolean(nullable: false),
                        ShowRelatedWebSites = c.Boolean(nullable: false),
                        ShowServices = c.Boolean(nullable: false),
                        ShowSliders = c.Boolean(nullable: false),
                        ShowSocialMedias = c.Boolean(nullable: false),
                        ShowTeams = c.Boolean(nullable: false),
                        MegaTags = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sliders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.String(),
                        TitleArabic = c.String(),
                        TitleEnglish = c.String(),
                        DescriptionArabic = c.String(),
                        DescriptionEnglish = c.String(),
                        Order = c.Int(nullable: false),
                        IsImage = c.Boolean(nullable: false),
                        Iframe = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UserCreateId = c.Int(nullable: false),
                        EditeTime = c.DateTime(nullable: false),
                        UserEditId = c.Int(nullable: false),
                        Link = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SocialsMedia",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameArabic = c.String(),
                        NameEnglish = c.String(),
                        Image = c.String(),
                        Link = c.String(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        UserCreateId = c.Int(nullable: false),
                        EditeTime = c.DateTime(nullable: false),
                        UserEditId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.String(),
                        NameArabic = c.String(),
                        NameEnglish = c.String(),
                        JobTitleArabic = c.String(),
                        JobTitleEnglish = c.String(),
                        JobDescriptionArabic = c.String(),
                        JobDescriptionEnglish = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UserCreateId = c.Int(nullable: false),
                        EditeTime = c.DateTime(nullable: false),
                        UserEditId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserForLogins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LoginName = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        FireBaseToken = c.String(),
                        Rememberme = c.Boolean(nullable: false),
                        CurrentTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WhoWeAre",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.String(),
                        TitleArabic = c.String(),
                        TitleEnglish = c.String(),
                        DescriptionArabic = c.String(),
                        DescriptionEnglish = c.String(),
                        EditeTime = c.DateTime(nullable: false),
                        UserEditId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WhyFilmed",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Image = c.String(),
                        TitleArabic = c.String(),
                        TitleEnglish = c.String(),
                        DescriptionArabic = c.String(),
                        DescriptionEnglish = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                        UserCreateId = c.Int(nullable: false),
                        EditeTime = c.DateTime(nullable: false),
                        UserEditId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ServiceImages", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.NewImages", "NewId", "dbo.News");
            DropForeignKey("dbo.logs", "UserId", "dbo.Users");
            DropForeignKey("dbo.LocationImages", "LocationId", "dbo.Locations");
            DropForeignKey("dbo.PrivilageRoutes", "PrivilageId", "dbo.Privilages");
            DropForeignKey("dbo.GroupPriviages", "PrivilageId", "dbo.Privilages");
            DropForeignKey("dbo.Users", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.GroupPriviages", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.FilmedImages", "FilmedId", "dbo.Filmed");
            DropForeignKey("dbo.BookingInServices", "BookingServiceId", "dbo.BookingServices");
            DropForeignKey("dbo.BookingInServices", "BookingId", "dbo.Bookings");
            DropForeignKey("dbo.Galaries", "AlbumId", "dbo.Albums");
            DropForeignKey("dbo.AgendaImages", "AgendaId", "dbo.Agenda");
            DropIndex("dbo.ServiceImages", new[] { "ServiceId" });
            DropIndex("dbo.NewImages", new[] { "NewId" });
            DropIndex("dbo.logs", new[] { "UserId" });
            DropIndex("dbo.LocationImages", new[] { "LocationId" });
            DropIndex("dbo.PrivilageRoutes", new[] { "PrivilageId" });
            DropIndex("dbo.Users", new[] { "GroupId" });
            DropIndex("dbo.GroupPriviages", new[] { "GroupId" });
            DropIndex("dbo.GroupPriviages", new[] { "PrivilageId" });
            DropIndex("dbo.FilmedImages", new[] { "FilmedId" });
            DropIndex("dbo.BookingInServices", new[] { "BookingId" });
            DropIndex("dbo.BookingInServices", new[] { "BookingServiceId" });
            DropIndex("dbo.Galaries", new[] { "AlbumId" });
            DropIndex("dbo.AgendaImages", new[] { "AgendaId" });
            DropTable("dbo.WhyFilmed");
            DropTable("dbo.WhoWeAre");
            DropTable("dbo.UserForLogins");
            DropTable("dbo.Teams");
            DropTable("dbo.SocialsMedia");
            DropTable("dbo.Sliders");
            DropTable("dbo.Settings");
            DropTable("dbo.Services");
            DropTable("dbo.ServiceImages");
            DropTable("dbo.RelatedWebSites");
            DropTable("dbo.Procedures");
            DropTable("dbo.News");
            DropTable("dbo.NewImages");
            DropTable("dbo.logs");
            DropTable("dbo.Locations");
            DropTable("dbo.LocationImages");
            DropTable("dbo.PrivilageRoutes");
            DropTable("dbo.Privilages");
            DropTable("dbo.Users");
            DropTable("dbo.Groups");
            DropTable("dbo.GroupPriviages");
            DropTable("dbo.FilmedImages");
            DropTable("dbo.Filmed");
            DropTable("dbo.Facilities");
            DropTable("dbo.Documents");
            DropTable("dbo.Customer");
            DropTable("dbo.ContactUs");
            DropTable("dbo.ConatctRequist");
            DropTable("dbo.Clients");
            DropTable("dbo.Citizens");
            DropTable("dbo.BookingServices");
            DropTable("dbo.Bookings");
            DropTable("dbo.BookingInServices");
            DropTable("dbo.Galaries");
            DropTable("dbo.Albums");
            DropTable("dbo.AgendaImages");
            DropTable("dbo.Agenda");
            DropTable("dbo.AboutUs");
        }
    }
}
