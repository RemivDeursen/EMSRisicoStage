﻿using UnityEngine;
using System;
using System.Collections;
using System.Globalization;

public class DirectoryBox {
	#region Variables
	private FolderBuilder folderBuilder;
	private FolderManager folderManager;

	private GameObject folderBox;
	private GameObject parent;
	private GUITexture backgroundTexture;
	public Texture2D backgroundImage;




	
	private string directoryName;
	private string directorNumber;
	
	private bool editActive;
	private bool fileMode = false;
	private bool Protected = false;
	#if UNITY_ANDROID
	TouchScreenKeyboard keyboard;
	#endif
	string newName;

	private float timer;
	private float timerPoint = 3f;
	
	private float x;
	private float y;
	private float boxX;
	private float boxY;
	private float width = 200;
	private float height = 200;
	private float startY;
	private int boxNumber;

	private int columns;

	private Rect boxCollider;
	private float testY;

	private float newStartY;

	private GameObject deleteIcon;
	private GUITexture deleteTexture;
	private Texture2D deleteImage;
	private Rect deleteRect;
	#endregion
	
	#region Porperties
	public float X{
		set{this.x = value;}
		get{return this.x;}
	}
	
	public float Y{
		set{this.y = value;}
		get{return this.y;}
	}

	public float TestY{
		set{this.testY = value;}
		get{return this.testY;}
	}
	
	public float BoxX{
		set{this.boxX = value;}
		get{return this.boxX;}
	}

	public float BoxY{
		set{this.boxY = value;}
		get{return this.boxY;}
	}

	public int Columns {
		set{this.columns = value;}
		get{return this.columns;}
	}

	public float StartY{
		set{this.startY = value;}
		get{return this.startY;}
	}

	public float NewStartY{
		set{this.newStartY = value;}
		get{return this.newStartY;}
	}

	public GameObject Parent{
		set{this.parent = value;}
		get{return this.parent;}
	}
	
	public int BoxNumber{
		set{this.boxNumber = value;}
		get{return this.boxNumber;}
	}
	
	public string DirectoryName{
		set{this.directoryName = value;}
		get{return this.directoryName;}
	}
	
	public string DirectorNumber{
		set{this.directorNumber = value;}
		get{return this.directorNumber;}
	}
	
	public bool FileMode {
		set{this.fileMode = value;}
		get{return this.fileMode;}
	}
	public bool ProtectedBox {
		set{this.Protected = value;}
		get{return this.Protected;}
	}
	
	public FolderManager FolderManagerScript{
		set{this.folderManager = value;}
	}
	
	public FolderBuilder folderBuilderScript{
		set{this.folderBuilder = value;}
	}
	public Rect BoxCollider{
		get{return this.boxCollider;}
	}
	public Rect DeleteRect{
		get{return this.deleteRect;}
	}
	#endregion
	
	// Use this for initialization
	public void Setup () {		
		deleteImage = Resources.Load("Icons/Play_Icons/Delete") as Texture2D;

		columns = 3;

		width = Screen.width / 5;
		height = Screen.height / 5;

		boxX = (Screen.width - ((width * columns) - width * x));
		boxY = (Screen.height - (height + (height * y)));
		TestY = (height * y);

		folderBox = new GameObject(directoryName);
		folderBox.transform.parent = parent.transform;


		//folderBox.transform.localScale = new Vector3(0,0,1);
		//folderBox.AddComponent<GUITexture>();
		//backgroundTexture = folderBox.GetComponent<GUITexture>();
		//backgroundTexture.texture = backgroundImage;
		//backgroundTexture.pixelInset = new Rect(boxX,boxY,width,height);
		//homeIconRect = new Rect(50,50, 19, 36);



		boxCollider = new Rect(boxX, TestY,width,height);
		Debug.Log("Backgrounds/" + BoxNumber);
			backgroundImage = Resources.Load("Backgrounds/" + BoxNumber) as Texture2D;
			backgroundTexture.texture = backgroundImage;

	}
	
	public void Update()
	{
		Vector2 mousePos = new Vector2(Input.mousePosition.x,Screen.height - Input.mousePosition.y); 

		backgroundTexture.pixelInset = new Rect(boxX,boxY,width,height);
		boxCollider = new Rect(boxX, TestY ,width,height);

		if(deleteIcon != null)
		{
			if(deleteIcon.name == "Delete_Icon") 
			{
				deleteTexture.pixelInset = new Rect(boxX + ((width / 2) - (76 / 2)),boxY + ((height / 2) - (86 / 2)),76,86);
				deleteRect = new Rect(boxX + ((width / 2) - (76 / 2)),TestY + ((height / 2) - (86 / 2)),76,86);

				if(Input.GetMouseButtonDown(0) && !boxCollider.Contains(mousePos))
				{
					deleteRect = new Rect();
					folderBuilder.DeleteObject(deleteIcon);

				}
				if(Input.GetMouseButtonDown(0) && deleteRect.Contains(mousePos))
				{
					Debug.Log("Delete: " + directoryName);
				}
			}
		}

		if(boxCollider.Contains(mousePos))
		{
			if (Input.GetMouseButtonDown(0)) {
				timer = 0;
				Debug.Log(folderBox.name);
			}

			if (Input.GetMouseButton(0)) {
				timer += 1* Time.deltaTime;
				if(timer > timerPoint)
				{
					if(GameObject.Find("Delete_Icon") == null)
					{
						Debug.Log(folderBox.name);
						deleteIcon = new GameObject();
						
						deleteIcon.name = "Delete_Icon";
						
						//Give Buttons the right components
						deleteIcon.AddComponent<GUITexture>();
						
						deleteIcon.transform.localScale = new Vector3(0,0,1);
						
						//Select the Components a variable
						deleteTexture = deleteIcon.GetComponent<GUITexture>();
						
						deleteTexture.texture = deleteImage;
						
						deleteTexture.pixelInset = new Rect(boxX + ((width / 2) - (76 / 2)),boxY + ((height / 2) - (86 / 2)),76,86);
						deleteRect = new Rect(boxX + ((width / 2) - (76 / 2)),TestY + ((height / 2) - (86 / 2)),76,86);
					}
				}
			}
			if (Input.GetMouseButtonUp(0)) {

			}
		} 
		if (editActive) {
			#if UNITY_ANDROID
			if (!Input.GetMouseButton(0)) {
				if (keyboard.done && !keyboard.wasCanceled) {
					DirectoryClickEdit (keyboard.text);
				}
				if (keyboard.active) {
					newName = keyboard.text;
				}
			}
#endif

		}
	}
	
	// Call this Function when clicked on the box
	private void DirectoryClick()
	{

	}
	
	// Call this Function when clicked on the Deletebox
	private void DirectoryDelete()
	{
		Debug.Log ("Delete_Folder: " + directoryName);
		folderManager.DeleteFolderWithName (directoryName);
		folderBuilder.rebuildFolderArray ();
	}
	
	// Call this Function when clicked on the Editbox
	private void DirectoryEdit()
	{
		Debug.Log ("Edit_Folder: " + directoryName);
		buildEdit ();
		//folderBuilder.rebuildArray ();
	}
	// Call this Function when clicked on the duplicate
	private void DirectoryDuplicate()
	{
		Debug.Log ("Duplicate_Folder: " + directoryName);
		folderManager.CopyByName (directoryName);
		folderBuilder.rebuildFolderArray ();
		//folderBuilder.rebuildArray ();
	}
	
	
	public void buildEdit(){
		editActive = true;
		#if UNITY_ANDROID
		keyboard = (TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, false));
#endif
	}
	
	void DirectoryClickEdit (string newName)
	{
		editActive = false;
		Debug.Log ("");
		folderManager.RenameFolderWithID (boxNumber, newName, folderBuilder.ExampleProjects.Count);
		folderBuilder.rebuildFolderArray ();
	}
	
	// Call this Function when clicked on the Playbox
	private void DirectoryPlay()
	{
		Debug.Log ("Play_Folder: " + directoryName);
	}
	
	// Call this Function to create this box
	public void DirectoryCreate () {
		Debug.Log ("Create_Folder");
		folderManager.CreateFolder();
		folderBuilder.rebuildFolderArray ();
	}
	
	// Call this Function to create this box
	public void DirectoryBack () {
		Debug.Log("Back");
		folderBuilder.CloseFolder ();
	}
	
	// Call this Function to create this box
	public void show () {
		Debug.Log ("Show_Folder");
	}
	
	// Call this Function to remove this box
	public void Remove () {
		boxCollider = new Rect();
	}
	
	// Call this Function when Selected
	public void Select () {
		Debug.Log ("Select_Folder");
	}
	
	// Call this Function when Select an other one
	public void Unselect () {

	}
}
