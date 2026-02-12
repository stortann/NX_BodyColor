' Journal desciption:
' 1/ Changes session's settings to Part Shininess
' 2/ Sets Background to Plain White (1.0, 1.0, 1.0)
' 3/ Changes all of the lights to 0.0 except "Ambient" which will be 1.0
' 4/ Turns off Shaded Edges 
' 5/ Hides everything that is not a body
' 6/ Changes colors of bodies according to a specific rule, look into code to find exact parameters
' Written in VB.Net
' Tested on Siemens NX 2412

Imports System
Imports NXOpen
Imports NXOpenUI
Imports NXOpen.UF
Imports NXOpen.Features

Module NXJournal
	Sub Main (ByVal args() As String) 

		Dim theSession As NXOpen.Session = NXOpen.Session.GetSession()
		Dim workPart As NXOpen.Part = theSession.Parts.Work
		Dim theUISession As UI = UI.GetUI
		Dim lw As ListingWindow = theSession.ListingWindow
		
		' lw.Open()
		' lw.WriteLine("Hi!")
		' lw.Close()
		

		' Part Shininess
		theSession.Preferences.VisualizationVisualPreferences.FinishEffectInShadedMode = NXOpen.Preferences.SessionVisualizationVisual.FinishEffect.PartShininess

		
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


		' Turn off Edges 
		workPart.ModelingViews.WorkView.VisualizationVisualPreferences.ShadedEdgeStyle = NXOpen.Preferences.ViewVisualizationVisual.ShadedEdgeStyleType.None


		' Hide everything that's not a body
		Dim numberHidden1 As Integer
		numberHidden1 = theSession.DisplayManager.HideByType(DisplayManager.ShowHideType.Sketches, _
		DisplayManager.ShowHideScope.AnyInAssembly)
		Dim numberHidden12 As Integer = Nothing
		numberHidden12 = theSession.DisplayManager.HideByType("SHOW_HIDE_TYPE_SKETCHES", _
		NXOpen.DisplayManager.ShowHideScope.AnyInAssembly)
		 
		Dim numberHidden2 As Integer
		numberHidden2 = theSession.DisplayManager.HideByType(DisplayManager.ShowHideType.Curves, _
		DisplayManager.ShowHideScope.AnyInAssembly)
		Dim numberHidden21 As Integer = Nothing
		numberHidden21 = theSession.DisplayManager.HideByType("SHOW_HIDE_TYPE_CURVES", _
		NXOpen.DisplayManager.ShowHideScope.AnyInAssembly)
		 
		Dim numberHidden3 As Integer
		numberHidden3 = theSession.DisplayManager.HideByType(DisplayManager.ShowHideType.Datums, _
		DisplayManager.ShowHideScope.AnyInAssembly)
		Dim numberHidden31 As Integer = Nothing
		numberHidden31 = theSession.DisplayManager.HideByType("SHOW_HIDE_TYPE_DATUM_PLANES", _
		NXOpen.DisplayManager.ShowHideScope.AnyInAssembly)

		Dim numberHidden4 As Integer
		numberHidden4 = theSession.DisplayManager.HideByType(DisplayManager.ShowHideType.Points, _
		DisplayManager.ShowHideScope.AnyInAssembly)
		Dim numberHidden41 As Integer = Nothing
		numberHidden41 = theSession.DisplayManager.HideByType("SHOW_HIDE_TYPE_POINTS", NXOpen.DisplayManager.ShowHideScope.AnyInAssembly)

		Dim numberHidden5 As Integer
		numberHidden5 = theSession.DisplayManager.HideByType(DisplayManager.ShowHideType.Csys, _
		DisplayManager.ShowHideScope.AnyInAssembly)
		Dim numberHidden51 As Integer = Nothing
		numberHidden51 = theSession.DisplayManager.HideByType("SHOW_HIDE_TYPE_CSYS", NXOpen.DisplayManager.ShowHideScope.AnyInAssembly)


		' Change colors of bodies
		' If a color is in "colors" it will be changed to color from "newColors" from the same index, otherwise nothing
		' red, yellow, blue, cyan
		Dim colors = New Integer() {186, 6, 211, 31}
		' white, medium gray, iron gray, charcoal grey
		Dim newColors = New Integer() {1, 159, 201, 210}

		Dim tempBodyColor aS Integer
		Dim tempBodyNewColor aS Integer
		Dim tempBodyColorIndex As Integer
		Dim count as integer
		dim tempFaceColor as integer
		
		
		For Each tempFeature As Features.Feature In workPart.Features
			
			Dim featureBodies() As DisplayableObject
			featureBodies = tempFeature.GetBodies()
						
			For Each tempBody As Body In featureBodies
				
				'Dim myBodies(0) As DisplayableObject
				'myBodies(0)=tempBody			 
				
				for each myFace as NXOpen.Face in tempBody.GetFaces
										
					tempFaceColor = myFace.Color					
					tempBodyColorIndex = Array.IndexOf(colors, tempFaceColor)
					
					If Not tempBodyColorIndex=-1 Then
						tempBodyNewColor=newColors(tempBodyColorIndex)
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

