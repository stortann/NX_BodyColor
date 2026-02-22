' Journal desciption:
' 1/ Turns off Translucency
' 2/ Changes session's settings to Part Shininess
' 3/ Turns off Shaded Edges 
' 4/ Sets Background to Plain White (1.0, 1.0, 1.0)
' 5/ Changes all of the lights to 0.0 except "Ambient" which will be 1.0
' 6/ Hides known things that are not a body - this code needs to be updated in case that it didn't hide something 
' 7/ Changes colors of bodies according to a specific rule, look into code to find exact parameters
' Written In VB.Net
' Tested on Siemens NX 2412


Imports System
Imports NXOpen
Imports NXOpenUI
Imports NXOpen.UF
Imports NXOpen.Features


Module NXJournal
	Sub Main (ByVal args() As String) 

		Dim theSession As NXOpen.Session = NXOpen.Session.GetSession()
		Dim theDisplayManager As DisplayManager = NXOpen.Session.GetSession().DisplayManager
		Dim workPart As NXOpen.Part = theSession.Parts.Work
		Dim theUISession As UI = UI.GetUI
		Dim lw As ListingWindow = theSession.ListingWindow		
		
		
		' If a color is In "colors" it will be changed to a color from "myColors" from the same index, otherwise nothing
		' Example: 186/red will be changed to 123/smoke gray, but not vice versa
		' red, yellow, blue, cyan
		Dim colors = New Integer() {186, 6, 211, 31}
		
		' smoke gray, medium gray, iron gray, charcoal grey
		Dim myColors = New Integer() {123, 159, 201, 210}

		
		' Translucency
		theSession.Preferences.VisualizationVisualPreferences.Translucency = False


		' Part Shininess
		theSession.Preferences.VisualizationVisualPreferences.FinishEffectInShadedMode = NXOpen.Preferences.SessionVisualizationVisual.FinishEffect.PartShininess


		' Turn off Edges 
		workPart.ModelingViews.WorkView.VisualizationVisualPreferences.ShadedEdgeStyle = NXOpen.Preferences.ViewVisualizationVisual.ShadedEdgeStyleType.None


		' Plain White Background
		Dim background1 As NXOpen.Display.Background = Nothing
		background1 = workPart.Views.CreateBackground(workPart.ModelingViews.WorkView, False)

		background1.BackgroundShadedViewsType = 1

		Dim plaincolor1(2) As Double
		plaincolor1(0) = 1.0
		plaincolor1(1) = 1.0
		plaincolor1(2) = 1.0
		background1.SetBackgroundShadedViewsPlain(plaincolor1)

		Dim nXObject1 As NXOpen.NXObject = Nothing
		nXObject1 = background1.Commit()

		background1.Destroy()


		' Changes all of the lights to 0.0 except "Ambient" which will be 1.0
		Dim lighting1 As NXOpen.Display.Lighting = Nothing
		lighting1 = workPart.Views.CreateLighting(workPart.ModelingViews.WorkView)


		Dim light1 As NXOpen.Light = CType(workPart.Lights.FindObject("Scene Ambient"), NXOpen.Light)
		Dim lightBuilder1 As NXOpen.Display.LightBuilder = Nothing
		lightBuilder1 = workPart.Views.CreateLightBuilder(light1)
		lighting1.SetLightBuilderInList(0, lightBuilder1)


		Dim light2 As NXOpen.Light = CType(workPart.Lights.FindObject("Scene Left Top"), NXOpen.Light)
		Dim lightBuilder2 As NXOpen.Display.LightBuilder = Nothing
		lightBuilder2 = workPart.Views.CreateLightBuilder(light2)
		lighting1.SetLightBuilderInList(1, lightBuilder2)


		Dim light3 As NXOpen.Light = CType(workPart.Lights.FindObject("Scene Right Top"), NXOpen.Light)
		Dim lightBuilder3 As NXOpen.Display.LightBuilder = Nothing
		lightBuilder3 = workPart.Views.CreateLightBuilder(light3)
		lighting1.SetLightBuilderInList(2, lightBuilder3)


		Dim light4 As NXOpen.Light = CType(workPart.Lights.FindObject("Scene Left Bottom"), NXOpen.Light)
		Dim lightBuilder4 As NXOpen.Display.LightBuilder = Nothing
		lightBuilder4 = workPart.Views.CreateLightBuilder(light4)
		lighting1.SetLightBuilderInList(3, lightBuilder4)


		Dim light5 As NXOpen.Light = CType(workPart.Lights.FindObject("Scene Right Bottom"), NXOpen.Light)
		Dim lightBuilder5 As NXOpen.Display.LightBuilder = Nothing
		lightBuilder5 = workPart.Views.CreateLightBuilder(light5)
		lighting1.SetLightBuilderInList(4, lightBuilder5)


		lighting1.LightsShadedViewsLightingCollection = NXOpen.Display.Lighting.LightingCollectionType.UserDefined

		lighting1.RemoveLightBuilderInList("Scene Right Bottom")

		lighting1.RemoveLightBuilderInList("Scene Left Bottom")

		lighting1.RemoveLightBuilderInList("Scene Right Top")

		lighting1.RemoveLightBuilderInList("Scene Left Top")

		lighting1.SetLightBuilderInList("Scene Ambient", 1.0)

		Dim nXObject2 As NXOpen.NXObject = Nothing
		nXObject2 = lighting1.Commit()

		lighting1.Destroy()


		' Hide known things that could be in a part that are not a body
		Dim typeHidden1 As Integer
		typeHidden1 = theDisplayManager.HideByType(DisplayManager.ShowHideType.Sketches, _
		DisplayManager.ShowHideScope.AnyInAssembly)
		Dim typeHidden12 As Integer = Nothing
		typeHidden12 = theDisplayManager.HideByType("SHOW_HIDE_TYPE_SKETCHES", _
		NXOpen.DisplayManager.ShowHideScope.AnyInAssembly)
		
		Dim typeHidden2 As Integer
		typeHidden2 = theDisplayManager.HideByType(DisplayManager.ShowHideType.Curves, _
		DisplayManager.ShowHideScope.AnyInAssembly)
		Dim typeHidden21 As Integer = Nothing
		typeHidden21 = theDisplayManager.HideByType("SHOW_HIDE_TYPE_CURVES", _
		NXOpen.DisplayManager.ShowHideScope.AnyInAssembly)
		
		Dim typeHidden3 As Integer
		typeHidden3 = theDisplayManager.HideByType(DisplayManager.ShowHideType.Datums, _
		DisplayManager.ShowHideScope.AnyInAssembly)
		Dim typeHidden31 As Integer = Nothing
		typeHidden31 = theDisplayManager.HideByType("SHOW_HIDE_TYPE_DATUM_PLANES", _
		NXOpen.DisplayManager.ShowHideScope.AnyInAssembly)

		Dim typeHidden4 As Integer
		typeHidden4 = theDisplayManager.HideByType(DisplayManager.ShowHideType.Points, _
		DisplayManager.ShowHideScope.AnyInAssembly)
		Dim typeHidden41 As Integer = Nothing
		typeHidden41 = theDisplayManager.HideByType("SHOW_HIDE_TYPE_POINTS", _
		NXOpen.DisplayManager.ShowHideScope.AnyInAssembly)

		Dim typeHidden5 As Integer
		typeHidden5 = theDisplayManager.HideByType(DisplayManager.ShowHideType.Csys, _
		DisplayManager.ShowHideScope.AnyInAssembly)
		Dim typeHidden51 As Integer = Nothing
		typeHidden51 = theDisplayManager.HideByType("SHOW_HIDE_TYPE_CSYS", _
		NXOpen.DisplayManager.ShowHideScope.AnyInAssembly)
		
		Dim typeHidden6 As Integer
		typeHidden6 = theDisplayManager.HideByType(DisplayManager.ShowHideType.DrawingAnnotation, _
		DisplayManager.ShowHideScope.AnyInAssembly)
		Dim typeHidden61 As Integer = Nothing
		typeHidden61 = theDisplayManager.HideByType("SHOW_HIDE_TYPE_DRAWING_ANNOTATION", _
		NXOpen.DisplayManager.ShowHideScope.AnyInAssembly)
		
		Dim typeHidden7 As Integer
		typeHidden7 = theDisplayManager.HideByType(DisplayManager.ShowHideType.PMI, _
		DisplayManager.ShowHideScope.AnyInAssembly)
		Dim typeHidden71 As Integer = Nothing
		typeHidden71 = theDisplayManager.HideByType("SHOW_HIDE_TYPE_PMI", _
		NXOpen.DisplayManager.ShowHideScope.AnyInAssembly)
		
		
		' ' If you ever need to hide some other group - look here for the type
        ' Dim myTypes() = theDisplayManager.GetShowableHideableTypes()
        ' lw.Open()
        ' lw.WriteLine("ShowableHideableTypes:")
        ' For Each myType As String In types
            ' lw.WriteLine(myType)
        ' Next
        ' lw.Close()
		
		
		' Change colors of bodies			
		' Values of changed colors to be changed are in the beginning, look for the "colors" variable
		Dim tempBodyColor As Integer
		Dim tempBodyNewColor As Integer
		Dim tempBodyColorIndex As Integer
		Dim count As Integer
		Dim tempFaceColor As Integer
		
		For Each tempFeature As Features.Feature In workPart.Features
			
			Dim featureBodies() As DisplayableObject
			featureBodies = tempFeature.GetBodies()
					
			For Each tempBody As Body In featureBodies
				
				'Dim myBodies(0) As DisplayableObject
				'myBodies(0)=tempBody			 
				
				For Each myFace As NXOpen.Face In tempBody.GetFaces
					
					
					tempFaceColor = myFace.Color					
					tempBodyColorIndex = Array.IndexOf(colors, tempFaceColor)
					
					If Not tempBodyColorIndex=-1 Then
						tempBodyNewColor=myColors(tempBodyColorIndex)
					Else
						tempBodyNewColor = tempFaceColor
					End If				
					
					
					myFace.Color = tempBodyNewColor
					myFace.RedisplayObject()
					
				Next
		
			Next			
			
		Next

	End Sub
End Module
