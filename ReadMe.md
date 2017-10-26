# Templating Ads

Many times customer facing apps need a free version. There is perhaps no more popular way of providing a "Free" app while still getting paid as a developer than to incorporate Ads in our Applications. AdMob gives us a great cross platform way of achieving In-App Ads in both Android and iOS.

Adding Ad's across an entire app can become tedious however trying to implement the same View Container. This is where the power of Control Templates in Xamarin Forms comes into play. Control Templates will allow you to create reusable View Layouts that you can use across your application.

## Prerequisites

Ensure that you have setup your machine for Xamarin Development. You can find the full suggested prerequisites on our [Meetups repo](https://github.com/SDXamarinDevs/Meetup).

You will also need to sign up for [AdMob](https://www.google.com/admob/).

## Challenge

### Level 0

- Create a new Control Template

<details>
    <summary>How to use a Control Template</summary>
There are two parts to using a Control Template. The first is to create the template in the Application ResourceDictionary. The second part is to set the template on a page.

```xml
<ResourceDictionary>
    <ControlTemplate x:Key="awesomeTemplate">
        <!-- Add your layout here.... note that you need to have a 
             ContentPresenter somewhere in your layout -->
    </ControlTemplate>
</ResourceDictionary>
```

Next you must add the `awesomeTemplate` to your View. To do this we will add the `StaticResource` in the ContentPage consuming the template. 

```xml
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             ControlTemplate="{StaticResource awesomeTemplate}"
             Title="{Binding Title}"
             x:Class="AwesomeApp.Views.ViewA">
```

</details>

<br/>

For your first Template we'll use a basic layout that you can use for something like a LoginPage.

<details>
    <summary>Sample Layout</summary>

```xml
<StackLayout>
    <BoxView BackgroundColor="{StaticResource PrimaryDark}">
    <ContentPresenter Padding="20" />
    <BoxView BackgroundColor="{StaticResource Primary}">
</StackLayout>
```

</details>

<br />

- Add a simple login form to the LoginPage like the following:

<details>
    <summary>Sample Login Form</summary>

```xml
<StackLayout>
    <Entry Placeholder="username" />
    <Entry Placeholder="password" IsPassword="True" />
    <Button Text="Login" Command="{Binding LoginCommand}" />
</StackLayout>
```

</details>

### Level 1

Now that you have an idea how Templates work let's get started on building our custom View to add AdMob ads to our app.

- Create an app in AdMob for the application not currently in the App Store. Create a simple banner ad. Be sure to copy the AdUnit Id and the App Id from AdMob
- Install the appropriate package on the platform you are developing for. Note there are different packages and different controls for each platform.
  - iOS: Xamarin.Google.iOS.MobileAds
  - Android: Xamarin.GooglePlayServices.Ads
- Initialize AdMob on the Platform
- Create a Simple View and add the renderer as shown below

<details>
    <summary>Platform Initialization</summary>

**iOS**

```cs
public override bool OnFinishedLaunching(UIApplication uiApplication, NSDictionary launchOptions)
{
    global::Xamarin.Forms.Forms.Init();
    MobileAds.Configure("{AdMob App Id}");
    return base.FinishedLaunching(uiApplication, launchOptions);
}
```

**Android**

```cs
protected override void OnCreate(Bundle savedInstanceState)
{
    TabLayoutResource = Resource.Layout.Tabbar;
    ToolbarResource = Resource.Layout.Toolbar;

    base.OnCreate(savedInstanceState);

    global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
    MobileAds.Initialize(ApplicationContext, "{AdMob App Id}");

    LoadApplication(new App(new AndroidInitializer()));
}
```

</details>

<br />

<details>
    <summary>Xamarin Forms Control &amp; Renderers</summary>

For level 1 there is really nothing that we need to do in our shared code other than create a custom class that inherits from View. This will allow us to create a Renderer that will actually be responsible for creating the native platform control. 

```cs
public class AdMobView : View
{
    // We will add more here in Level 2
}
```

**iOS**

```cs
using CoreGraphics;
using Google.MobileAds;
using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using ShowMeTheMoney.UI;
using ShowMeTheMoney.Views;

[assembly: ExportRenderer(typeof(ShowMeTheMoney.Controls.AdMobView), typeof(ShowMeTheMoney.iOS.Renderers.AdMobViewRenderer))]
namespace ShowMeTheMoney.iOS.Renderers
{
    public class AdMobViewRenderer : ViewRenderer<Controls.AdMobView, BannerView>
    {
        BannerView adView;
        BannerView CreateNativeAdControl()
        {
            if (adView != null)
                return adView;

            // TODO: Set this from a property
            string bannerId = "Your Ad Unit Id";

            // TODO: Set this from a property
            var adSize = AdSizeCons.Banner;
            // Setup your BannerView, review AdSizeCons class for more Ad sizes. 
            adView = new BannerView(size: adsize,
                                    origin: new CGPoint(0, UIScreen.MainScreen.Bounds.Size.Height - adSize.Size.Height))
            {
                AdUnitID = bannerId,
                RootViewController = GetVisibleViewController()
            };

            // Wire AdReceived event to know when the Ad is ready to be displayed
            adView.AdReceived += (object sender, EventArgs e) =>
            {
                //ad has come in
            };

            adView.LoadRequest(GetRequest());
            return adView;
        }

        Request GetRequest()
        {
            var request = Request.GetDefaultRequest();
            // Requests test ads on devices you specify. Your test device ID is printed to the console when
            // an ad request is made. GADBannerView automatically returns test ads when running on a
            // simulator. After you get your device ID, add it here
            //request.TestDevices = new [] { Request.SimulatorId.ToString () };
            return request;
        }

        /// 
        /// Gets the visible view controller.
        /// 
        /// The visible view controller.
        UIViewController GetVisibleViewController()
        {
            var rootController = UIApplication.SharedApplication.KeyWindow.RootViewController;

            if (rootController.PresentedViewController == null)
                return rootController;

            if (rootController.PresentedViewController is UINavigationController)
            {
                return ((UINavigationController)rootController.PresentedViewController).VisibleViewController;
            }

            if (rootController.PresentedViewController is UITabBarController)
            {
                return ((UITabBarController)rootController.PresentedViewController).SelectedViewController;
            }

            return rootController.PresentedViewController;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Controls.AdMobView> e)
        {
            base.OnElementChanged(e);
            if(Control == null)
            {
                CreateNativeAdControl();
                SetNativeControl(adView);
            }
        }
    }
}
```

**Android**

```cs
using Android.Widget;
using Android.Gms.Ads;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ShowMeTheMoney.Controls.AdMobView), typeof(ShowMeTheMoney.Droid.Renderers.AdMobViewRenderer))]
namespace ShowMeTheMoney.Droid.Renderers
{
    public class AdMobViewRenderer : ViewRenderer<Controls.AdMobView, AdView>
    {
        //TODO: Change the AdSize based on a property set by the AdMobView
        AdSize adSize = AdSize.SmartBanner;
        AdView adView;

        AdView CreateNativeAdControl()
        {
            if(adView != null)
                return adView;

            adView = new AdView(Forms.Context);
            adView.AdSize = adSize;
            //TODO: Change this based on a property set by the AdMobView
            adView.AdUnitId = "{Your AdUnit Id}";

            var adParams = new LinearLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);

            adView.LayoutParameters = adParams;

            adView.LoadAd(new AdRequest
                            .Builder()
                            .Build());
            return adView;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Controls.AdMobView> e)
        {
            base.OnElementChanged(e);
            if(Control == null)
            {
                CreateNativeAdControl();
                SetNativeControl(adView);
            }
        }
    }
}

```

</details>

### Level 2

- Create a couple of more ads on AdMob making sure to copy the AdUnit Id's. Make it more fun by adding a few different types of ads as well.
- Create an enum value for some different ad types
- Add a BindableProperty to your AdMobView for the `AdUnitId` and the `AdType`

As you will notice below you must create a static `readonly` BindableProperty for any property that you will bind to in XAML. This will contain the name of the property (as you will see in XAML), the expected value type, the type the BindableProperty is binding to, and a default value. It can then be get and set using the GetValue and SetValue methods as shown below.

```cs
public class SomeView : View
{
    public static readonly BindableProperty FooProperty =
        BindableProperty.Create(nameof(Foo), typeof(string), typeof(SomeView), null);

    public string Foo
    {
        get => (string)GetValue(FooProperty);
        set => SetValue(FooProperty, value);
    }
}
```

- Update your Renderer to set the AdSize based on your enum, and the AdUnitId based on the bindable property
- Set the AdUnitId in your ViewModel and bind to it in your XAML like `AdUnitId="{Binding AdUnitId}"`

### Level 3

- Create a new ControlTemplate that uses a Grid with 2 rows with row 0 having a `*` height and row having it's height set to `Auto`
- Place an AdMobView in row 1 of your control template and set the AdSize
- Now add a binding for the AdUnitId.*

*NOTE:
Bindings in a Control Template work a little different. First you will want to use the `TemplateBinding` instead of a traditional `Binding`. Also your binding context will be the Page itself. This will mean that in order to access a property in your ViewModel you will need a binding like: `SomeProperty="{TemplateBinding BindingContext.SomeProperty}"`

Do Not expand the next section until after you have run the application.

<details>
    <summary>Run the app before continuing to the Boss Level</summary>

<br />

It didn't work did it? The problem isn't the binding but rather the sequence in which the bindings take effect. This means that you must either find another way to set the Control, or get the AdUnitId.

### Boss Level

Let's take another look at BindableProperties. While a normal BindableProperty is applied directly to an element that we have control of such as a Page or a View, we can also create an `AttachedProperty` that could be attached to any `BindableObject`. So how does this look?

```cs
public static class Foo
{
    public static readonly BindableProperty FooProperty =
        BindableProperty.CreateAttached(nameof(Foo), typeof(string), typeof(BindableObject), null);

    public static string GetFoo(BindableObject bindable) =>
        (string)bindable.GetValue(FooProperty);
}
```

- Create an AttachedProperty to contain the AdUnitId and add it to your Views using the AdMob template you created
- Update your renderer to recurse the Parent until it finds a page and get the AdUnitId if the Element's AdUnitId is null or empty.

</details>