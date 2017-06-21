using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KinectGestures
{

	public interface GestureListenerInterface
	{
		// Invoked when a new user is detected and tracking starts
		// Here you can start gesture detection with KinectManager.DetectGesture()
		void UserDetected(uint userId, int userIndex);

		// Invoked when a user is lost
		// Gestures for this user are cleared automatically, but you can free the used resources
		void UserLost(uint userId, int userIndex);

		// Invoked when a gesture is in progress
		void GestureInProgress(uint userId, int userIndex, Gestures gesture, float progress,
		                       KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos);

		// Invoked if a gesture is completed.
		// Returns true, if the gesture detection must be restarted, false otherwise
		bool GestureCompleted(uint userId, int userIndex, Gestures gesture,
		                      KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos);

		// Invoked if a gesture is cancelled.
		// Returns true, if the gesture detection must be retarted, false otherwise
		bool GestureCancelled(uint userId, int userIndex, Gestures gesture,
		                      KinectWrapper.NuiSkeletonPositionIndex joint);
	}


	public enum Gestures
	{
		None = 0,
		GolpeDerecha,
		GolpeIzquierda,
		Musibidachi,
		Joe,
		Kamae,
		Zenkutsu,
		Kokutsu,
		Tsuki,
		OiZuki,
		Uke,
		AgeUke

	}


	public struct GestureData
	{
		public uint userId;
		public Gestures gesture;
		public int state;
		public float timestamp;
		public int joint;
		public Vector3 jointPos;
		public Vector3 screenPos;
		public float tagFloat;
		public Vector3 tagVector;
		public Vector3 tagVector2;
		public float progress;
		public bool complete;
		public bool cancelled;
		public List<Gestures> checkForGestures;
		public float startTrackingAtTime;
	}



	// Gesture related constants, variables and functions
	private const int leftHandIndex = (int)KinectWrapper.NuiSkeletonPositionIndex.HandLeft;
	private const int rightHandIndex = (int)KinectWrapper.NuiSkeletonPositionIndex.HandRight;

	private const int leftElbowIndex = (int)KinectWrapper.NuiSkeletonPositionIndex.ElbowLeft;
	private const int rightElbowIndex = (int)KinectWrapper.NuiSkeletonPositionIndex.ElbowRight;

	private const int leftShoulderIndex = (int)KinectWrapper.NuiSkeletonPositionIndex.ShoulderLeft;
	private const int rightShoulderIndex = (int)KinectWrapper.NuiSkeletonPositionIndex.ShoulderRight;

	private const int hipCenterIndex = (int)KinectWrapper.NuiSkeletonPositionIndex.HipCenter;
	private const int shoulderCenterIndex = (int)KinectWrapper.NuiSkeletonPositionIndex.ShoulderCenter;
	private const int leftHipIndex = (int)KinectWrapper.NuiSkeletonPositionIndex.HipLeft;
	private const int rightHipIndex = (int)KinectWrapper.NuiSkeletonPositionIndex.HipRight;


	private static void SetGestureJoint(ref GestureData gestureData, float timestamp, int joint, Vector3 jointPos)
	{
		gestureData.joint = joint;
		gestureData.jointPos = jointPos;
		gestureData.timestamp = timestamp;
		gestureData.state++;
	}

	private static void SetGestureCancelled(ref GestureData gestureData)
	{
		gestureData.state = 0;
		gestureData.progress = 0f;
		gestureData.cancelled = true;
	}

	private static void CheckPoseComplete(ref GestureData gestureData, float timestamp, Vector3 jointPos, bool isInPose, float durationToComplete)
	{
		if(isInPose)
		{
			float timeLeft = timestamp - gestureData.timestamp;
			gestureData.progress = durationToComplete > 0f ? Mathf.Clamp01(timeLeft / durationToComplete) : 1.0f;

			if(timeLeft >= durationToComplete)
			{
				gestureData.timestamp = timestamp;
				gestureData.jointPos = jointPos;
				gestureData.state++;
				gestureData.complete = true;
			}
		}
		else
		{
			SetGestureCancelled(ref gestureData);
		}
	}

	private static void SetScreenPos(uint userId, ref GestureData gestureData, ref Vector3[] jointsPos, ref bool[] jointsTracked)
	{
		Vector3 handPos = jointsPos[rightHandIndex];
//		Vector3 elbowPos = jointsPos[rightElbowIndex];
//		Vector3 shoulderPos = jointsPos[rightShoulderIndex];
		bool calculateCoords = false;

		if(gestureData.joint == rightHandIndex)
		{
			if(jointsTracked[rightHandIndex] /**&& jointsTracked[rightElbowIndex] && jointsTracked[rightShoulderIndex]*/)
			{
				calculateCoords = true;
			}
		}
		else if(gestureData.joint == leftHandIndex)
		{
			if(jointsTracked[leftHandIndex] /**&& jointsTracked[leftElbowIndex] && jointsTracked[leftShoulderIndex]*/)
			{
				handPos = jointsPos[leftHandIndex];
//				elbowPos = jointsPos[leftElbowIndex];
//				shoulderPos = jointsPos[leftShoulderIndex];

				calculateCoords = true;
			}
		}

		if(calculateCoords)
		{
//			if(gestureData.tagFloat == 0f || gestureData.userId != userId)
//			{
//				// get length from shoulder to hand (screen range)
//				Vector3 shoulderToElbow = elbowPos - shoulderPos;
//				Vector3 elbowToHand = handPos - elbowPos;
//				gestureData.tagFloat = (shoulderToElbow.magnitude + elbowToHand.magnitude);
//			}

			if(jointsTracked[hipCenterIndex] && jointsTracked[shoulderCenterIndex] &&
				jointsTracked[leftShoulderIndex] && jointsTracked[rightShoulderIndex])
			{
				Vector3 neckToHips = jointsPos[shoulderCenterIndex] - jointsPos[hipCenterIndex];
				Vector3 rightToLeft = jointsPos[rightShoulderIndex] - jointsPos[leftShoulderIndex];

				gestureData.tagVector2.x = rightToLeft.x; // * 1.2f;
				gestureData.tagVector2.y = neckToHips.y; // * 1.2f;

				if(gestureData.joint == rightHandIndex)
				{
					gestureData.tagVector.x = jointsPos[rightShoulderIndex].x - gestureData.tagVector2.x / 2;
					gestureData.tagVector.y = jointsPos[hipCenterIndex].y;
				}
				else
				{
					gestureData.tagVector.x = jointsPos[leftShoulderIndex].x - gestureData.tagVector2.x / 2;
					gestureData.tagVector.y = jointsPos[hipCenterIndex].y;
				}
			}

//			Vector3 shoulderToHand = handPos - shoulderPos;
//			gestureData.screenPos.x = Mathf.Clamp01((gestureData.tagFloat / 2 + shoulderToHand.x) / gestureData.tagFloat);
//			gestureData.screenPos.y = Mathf.Clamp01((gestureData.tagFloat / 2 + shoulderToHand.y) / gestureData.tagFloat);

			if(gestureData.tagVector2.x != 0 && gestureData.tagVector2.y != 0)
			{
				Vector3 relHandPos = handPos - gestureData.tagVector;
				gestureData.screenPos.x = Mathf.Clamp01(relHandPos.x / gestureData.tagVector2.x);
				gestureData.screenPos.y = Mathf.Clamp01(relHandPos.y / gestureData.tagVector2.y);
			}

			//Debug.Log(string.Format("{0} - S: {1}, H: {2}, SH: {3}, L : {4}", gestureData.gesture, shoulderPos, handPos, shoulderToHand, gestureData.tagFloat));
		}
	}

	private static void SetZoomFactor(uint userId, ref GestureData gestureData, float initialZoom, ref Vector3[] jointsPos, ref bool[] jointsTracked)
	{
		Vector3 vectorZooming = jointsPos[rightHandIndex] - jointsPos[leftHandIndex];

		if(gestureData.tagFloat == 0f || gestureData.userId != userId)
		{
			gestureData.tagFloat = 0.5f; // this is 100%
		}

		float distZooming = vectorZooming.magnitude;
		gestureData.screenPos.z = initialZoom + (distZooming / gestureData.tagFloat);
	}

//	private static void SetWheelRotation(uint userId, ref GestureData gestureData, Vector3 initialPos, Vector3 currentPos)
//	{
//		float angle = Vector3.Angle(initialPos, currentPos) * Mathf.Sign(currentPos.y - initialPos.y);
//		gestureData.screenPos.z = angle;
//	}

	// estimate the next state and completeness of the gesture
	public static void CheckForGesture(uint userId, ref GestureData gestureData, float timestamp, ref Vector3[] jointsPos, ref bool[] jointsTracked)
	{
		if(gestureData.complete)
			return;

		float bandSize = (jointsPos[shoulderCenterIndex].y - jointsPos[hipCenterIndex].y);
		float gestureTop = jointsPos[shoulderCenterIndex].y + bandSize / 2;
		float gestureBottom = jointsPos[shoulderCenterIndex].y - bandSize;
		float gestureRight = jointsPos[rightHipIndex].x;
		float gestureLeft = jointsPos[leftHipIndex].x;

		switch(gestureData.gesture)
		{
				case Gestures.GolpeDerecha:
				switch(gestureData.state)
				{
					case 0:  // gesture detection - phase 1
						if(jointsTracked[rightHandIndex] && jointsTracked[leftElbowIndex] && jointsTracked[rightShoulderIndex] &&
								(jointsPos[rightHandIndex].y - jointsPos[leftElbowIndex].y) > -0.1f &&
								Mathf.Abs(jointsPos[rightHandIndex].x - jointsPos[rightShoulderIndex].x) < 0.2f &&
								(jointsPos[rightHandIndex].z - jointsPos[leftElbowIndex].z) < -0.2f)
						{
							SetGestureJoint(ref gestureData, timestamp, rightHandIndex, jointsPos[rightHandIndex]);
							gestureData.progress = 0.5f;
						}
						break;

					case 1:  // gesture phase 2 = complete
						if((timestamp - gestureData.timestamp) < 1.5f)
						{
							bool isInPose = gestureData.joint == rightHandIndex ?
								jointsTracked[rightHandIndex] && jointsTracked[leftElbowIndex] && jointsTracked[rightShoulderIndex] &&
								(jointsPos[rightHandIndex].y - jointsPos[leftElbowIndex].y) > -0.1f &&
								Mathf.Abs(jointsPos[rightHandIndex].x - gestureData.jointPos.x) < 0.2f &&
								(jointsPos[rightHandIndex].z - gestureData.jointPos.z) < -0.1f :
								jointsTracked[leftHandIndex] && jointsTracked[rightElbowIndex] && jointsTracked[leftShoulderIndex] &&
								(jointsPos[leftHandIndex].y - jointsPos[rightElbowIndex].y) > -0.1f &&
								Mathf.Abs(jointsPos[leftHandIndex].x - gestureData.jointPos.x) < 0.2f &&
								(jointsPos[leftHandIndex].z - gestureData.jointPos.z) < -0.1f;

							if(isInPose)
							{
								Vector3 jointPos = jointsPos[gestureData.joint];
								CheckPoseComplete(ref gestureData, timestamp, jointPos, isInPose, 0f);
							}
						}
						else
						{
							// cancel the gesture
							SetGestureCancelled(ref gestureData);
						}
						break;
				}
				break;

				case Gestures.GolpeIzquierda:
					switch(gestureData.state)
					{
						case 0:  // gesture detection - phase 1
							if(jointsTracked[leftHandIndex] && jointsTracked[rightElbowIndex] && jointsTracked[leftShoulderIndex] &&
									(jointsPos[leftHandIndex].y - jointsPos[rightElbowIndex].y) > -0.1f &&
									Mathf.Abs(jointsPos[leftHandIndex].x - jointsPos[leftShoulderIndex].x) < 0.2f &&
									(jointsPos[leftHandIndex].z - jointsPos[rightElbowIndex].z) < -0.2f)
							{
								SetGestureJoint(ref gestureData, timestamp, leftHandIndex, jointsPos[leftHandIndex]);
								gestureData.progress = 0.5f;
							}
							break;

						case 1:  // gesture phase 2 = complete
							if((timestamp - gestureData.timestamp) < 1.5f)
							{
								bool isInPose = gestureData.joint == rightHandIndex ?
									jointsTracked[rightHandIndex] && jointsTracked[leftElbowIndex] && jointsTracked[rightShoulderIndex] &&
									(jointsPos[rightHandIndex].y - jointsPos[leftElbowIndex].y) > -0.1f &&
									Mathf.Abs(jointsPos[rightHandIndex].x - gestureData.jointPos.x) < 0.2f &&
									(jointsPos[rightHandIndex].z - gestureData.jointPos.z) < -0.1f :
									jointsTracked[leftHandIndex] && jointsTracked[rightElbowIndex] && jointsTracked[leftShoulderIndex] &&
									(jointsPos[leftHandIndex].y - jointsPos[rightElbowIndex].y) > -0.1f &&
									Mathf.Abs(jointsPos[leftHandIndex].x - gestureData.jointPos.x) < 0.2f &&
									(jointsPos[leftHandIndex].z - gestureData.jointPos.z) < -0.1f;

								if(isInPose)
								{
									Vector3 jointPos = jointsPos[gestureData.joint];
									CheckPoseComplete(ref gestureData, timestamp, jointPos, isInPose, 0f);
								}
							}
							else
							{
								// cancel the gesture
								SetGestureCancelled(ref gestureData);
							}
							break;
					}
					break;

					case Gestures.Musibidachi:
					  switch(gestureData.state)
					  {
					    case 0:  // gesture detection - phase 1
					      if(jointsTracked[hipCenterIndex] && (jointsPos[hipCenterIndex].y > -0.1f))
					      {
					        SetGestureJoint(ref gestureData, timestamp, hipCenterIndex, jointsPos[hipCenterIndex]);
					        gestureData.progress = 0.5f;
					      }
					      break;

					    case 1:  // gesture phase 2 = complete
					      if((timestamp - gestureData.timestamp) < 1.5f)
					      {
					        bool isInPose = jointsTracked[hipCenterIndex] &&
					          (jointsPos[hipCenterIndex].y - gestureData.jointPos.y) < -0.15f &&
					          Mathf.Abs(jointsPos[hipCenterIndex].x - gestureData.jointPos.x) < 0.2f;

					        if(isInPose)
					        {
					          Vector3 jointPos = jointsPos[gestureData.joint];
					          CheckPoseComplete(ref gestureData, timestamp, jointPos, isInPose, 0f);
					        }
					      }
					      else
					      {
					        // cancel the gesture
					        SetGestureCancelled(ref gestureData);
					      }
					      break;
					  }
					  break;

					case Gestures.Joe:
					switch(gestureData.state)
					{
						case 0:  // gesture detection - phase 1
							if(jointsTracked[rightHandIndex] && jointsTracked[leftElbowIndex] && jointsTracked[rightShoulderIndex] &&
                                jointsTracked[leftHandIndex] && jointsTracked[rightElbowIndex] && jointsTracked[leftShoulderIndex] &&
                                    (jointsPos[rightHandIndex].y - jointsPos[leftElbowIndex].y) > -0.1f &&
                                    (jointsPos[leftHandIndex].y - jointsPos[rightElbowIndex].y) > -0.1f &&
                                    Mathf.Abs(jointsPos[rightHandIndex].x - jointsPos[rightShoulderIndex].x) < 0.2f &&
									(jointsPos[rightHandIndex].z - jointsPos[leftElbowIndex].z) < -0.2f 
                                    &&
                                    Mathf.Abs(jointsPos[leftHandIndex].x - jointsPos[leftShoulderIndex].x) < 0.2f &&
                                    (jointsPos[leftHandIndex].z - jointsPos[rightElbowIndex].z) < -0.2f
                                    )
							{
								SetGestureJoint(ref gestureData, timestamp, rightHandIndex, jointsPos[rightHandIndex]);
								gestureData.progress = 0.5f;
							}

							break;

						case 1:  // gesture phase 2 = complete
							if((timestamp - gestureData.timestamp) < 1.5f)
							{
								bool isInPose = gestureData.joint == rightHandIndex ?
									jointsTracked[rightHandIndex] && jointsTracked[leftElbowIndex] && jointsTracked[rightShoulderIndex] &&
									(jointsPos[rightHandIndex].y - jointsPos[leftElbowIndex].y) > -0.1f &&
									Mathf.Abs(jointsPos[rightHandIndex].x - gestureData.jointPos.x) < 0.2f &&
									(jointsPos[rightHandIndex].z - gestureData.jointPos.z) < -0.1f :
									jointsTracked[leftHandIndex] && jointsTracked[rightElbowIndex] && jointsTracked[leftShoulderIndex] &&
									(jointsPos[leftHandIndex].y - jointsPos[rightElbowIndex].y) > -0.1f &&
									Mathf.Abs(jointsPos[leftHandIndex].x - gestureData.jointPos.x) < 0.2f &&
									(jointsPos[leftHandIndex].z - gestureData.jointPos.z) < -0.1f;

								if(isInPose)
								{
									Vector3 jointPos = jointsPos[gestureData.joint];
									CheckPoseComplete(ref gestureData, timestamp, jointPos, isInPose, 0f);
								}
							}
							else
							{
								// cancel the gesture
								SetGestureCancelled(ref gestureData);
							}
							break;
					}
					break;

						case Gestures.Kamae:
						switch(gestureData.state)
					  {
					    case 0:  // gesture detection - phase 1
					      if(jointsTracked[rightHandIndex] && jointsTracked[leftElbowIndex] && jointsTracked[rightShoulderIndex] &&
					         (jointsPos[rightHandIndex].y - jointsPos[leftElbowIndex].y) > -0.1f &&
					         Mathf.Abs(jointsPos[rightHandIndex].x - jointsPos[rightShoulderIndex].x) < 0.2f &&
					         (jointsPos[rightHandIndex].z - jointsPos[leftElbowIndex].z) < -0.3f)
					      {
					        SetGestureJoint(ref gestureData, timestamp, rightHandIndex, jointsPos[rightHandIndex]);
					        gestureData.progress = 0.5f;
					      }
					      else if(jointsTracked[leftHandIndex] && jointsTracked[rightElbowIndex] && jointsTracked[leftShoulderIndex] &&
					              (jointsPos[leftHandIndex].y - jointsPos[rightElbowIndex].y) > -0.1f &&
					              Mathf.Abs(jointsPos[leftHandIndex].x - jointsPos[leftShoulderIndex].x) < 0.2f &&
					              (jointsPos[leftHandIndex].z - jointsPos[rightElbowIndex].z) < -0.3f)
					      {
					        SetGestureJoint(ref gestureData, timestamp, leftHandIndex, jointsPos[leftHandIndex]);
					        gestureData.progress = 0.5f;
					      }
					      break;

					    case 1:  // gesture phase 2 = complete
					      if((timestamp - gestureData.timestamp) < 1.5f)
					      {
					        bool isInPose = gestureData.joint == rightHandIndex ?
					          jointsTracked[rightHandIndex] && jointsTracked[leftElbowIndex] && jointsTracked[rightShoulderIndex] &&
					          (jointsPos[rightHandIndex].y - jointsPos[leftElbowIndex].y) > -0.1f &&
					          Mathf.Abs(jointsPos[rightHandIndex].x - gestureData.jointPos.x) < 0.2f &&
					          (jointsPos[rightHandIndex].z - gestureData.jointPos.z) > 0.1f :
					          jointsTracked[leftHandIndex] && jointsTracked[rightElbowIndex] && jointsTracked[leftShoulderIndex] &&
					          (jointsPos[leftHandIndex].y - jointsPos[rightElbowIndex].y) > -0.1f &&
					          Mathf.Abs(jointsPos[leftHandIndex].x - gestureData.jointPos.x) < 0.2f &&
					          (jointsPos[leftHandIndex].z - gestureData.jointPos.z) > 0.1f;

					        if(isInPose)
					        {
					          Vector3 jointPos = jointsPos[gestureData.joint];
					          CheckPoseComplete(ref gestureData, timestamp, jointPos, isInPose, 0f);
					        }
					      }
					      else
					      {
					        // cancel the gesture
					        SetGestureCancelled(ref gestureData);
					      }
					      break;
					  }
					  break;


						case Gestures.Zenkutsu:
						Vector3 vectorWheel = (Vector3)jointsPos[rightHandIndex] - jointsPos[leftHandIndex];
						float distWheel = vectorWheel.magnitude;

					//				Debug.Log(string.Format("{0}. Dist: {1:F1}, Tag: {2:F1}, Diff: {3:F1}", gestureData.state,
					//				                        distWheel, gestureData.tagFloat, Mathf.Abs(distWheel - gestureData.tagFloat)));

						switch(gestureData.state)
						{
							case 0:  // gesture detection - phase 1
								if(jointsTracked[leftHandIndex] && jointsTracked[rightHandIndex] && jointsTracked[hipCenterIndex] && jointsTracked[shoulderCenterIndex] && jointsTracked[leftHipIndex] && jointsTracked[rightHipIndex] &&
									 jointsPos[leftHandIndex].y >= gestureBottom && jointsPos[leftHandIndex].y <= gestureTop &&
									 jointsPos[rightHandIndex].y >= gestureBottom && jointsPos[rightHandIndex].y <= gestureTop &&
									 distWheel >= 0.3f && distWheel < 0.7f)
								{
									SetGestureJoint(ref gestureData, timestamp, rightHandIndex, jointsPos[rightHandIndex]);
									gestureData.tagVector = Vector3.right;
									gestureData.tagFloat = distWheel;
									gestureData.progress = 0.3f;
								}
								break;

							case 1:  // gesture phase 2 = turning wheel
								if((timestamp - gestureData.timestamp) < 1.5f)
								{
									float angle = Vector3.Angle(gestureData.tagVector, vectorWheel) * Mathf.Sign(vectorWheel.y - gestureData.tagVector.y);
									bool isInPose = jointsTracked[leftHandIndex] && jointsTracked[rightHandIndex] && jointsTracked[hipCenterIndex] && jointsTracked[shoulderCenterIndex] && jointsTracked[leftHipIndex] && jointsTracked[rightHipIndex] &&
										jointsPos[leftHandIndex].y >= gestureBottom && jointsPos[leftHandIndex].y <= gestureTop &&
										jointsPos[rightHandIndex].y >= gestureBottom && jointsPos[rightHandIndex].y <= gestureTop &&
										distWheel >= 0.3f && distWheel < 0.7f &&
										Mathf.Abs(distWheel - gestureData.tagFloat) < 0.1f;

									if(isInPose)
									{
										//SetWheelRotation(userId, ref gestureData, gestureData.tagVector, vectorWheel);
										gestureData.screenPos.z = angle;  // wheel angle
										gestureData.timestamp = timestamp;
										gestureData.tagFloat = distWheel;
										gestureData.progress = 0.7f;
									}
								}
								else
								{
									// cancel the gesture
									SetGestureCancelled(ref gestureData);
								}
								break;

						}
						break;

						case Gestures.Kokutsu:
						switch(gestureData.state)
						{
							case 0:  // gesture detection - phase 1
								if(jointsTracked[rightHandIndex] && jointsTracked[leftShoulderIndex] &&
										 (jointsPos[rightHandIndex].y - jointsPos[leftShoulderIndex].y) > 0.05f)
								{
									SetGestureJoint(ref gestureData, timestamp, rightHandIndex, jointsPos[rightHandIndex]);
									gestureData.progress = 0.5f;
								}
								else if(jointsTracked[leftHandIndex] && jointsTracked[rightShoulderIndex] &&
													(jointsPos[leftHandIndex].y - jointsPos[rightShoulderIndex].y) > 0.05f)
								{
									SetGestureJoint(ref gestureData, timestamp, leftHandIndex, jointsPos[leftHandIndex]);
									gestureData.progress = 0.5f;
								}
								break;

							case 1:  // gesture phase 2 = complete
								if((timestamp - gestureData.timestamp) < 1.5f)
								{
									bool isInPose = gestureData.joint == rightHandIndex ?
										jointsTracked[rightHandIndex] && jointsTracked[leftElbowIndex] &&
										(jointsPos[rightHandIndex].y - jointsPos[leftElbowIndex].y) < -0.15f &&
										Mathf.Abs(jointsPos[rightHandIndex].x - gestureData.jointPos.x) <= 0.1f :
										jointsTracked[leftHandIndex] && jointsTracked[rightElbowIndex] &&
										(jointsPos[leftHandIndex].y - jointsPos[rightElbowIndex].y) < -0.15f &&
										Mathf.Abs(jointsPos[leftHandIndex].x - gestureData.jointPos.x) <= 0.1f;

									if(isInPose)
									{
										Vector3 jointPos = jointsPos[gestureData.joint];
										CheckPoseComplete(ref gestureData, timestamp, jointPos, isInPose, 0f);
									}
								}
								else
								{
									// cancel the gesture
									SetGestureCancelled(ref gestureData);
								}
								break;
						}
						break;


						case Gestures.Tsuki:
                        switch (gestureData.state)
                        {
                            case 0:  // gesture detection - phase 1
                                if (jointsTracked[rightHandIndex] && jointsTracked[leftElbowIndex] && jointsTracked[rightShoulderIndex] &&
                                        (jointsPos[rightHandIndex].y - jointsPos[leftElbowIndex].y) > -0.1f &&
                                        Mathf.Abs(jointsPos[rightHandIndex].x - jointsPos[rightShoulderIndex].x) < 0.2f &&
                                        (jointsPos[rightHandIndex].z - jointsPos[leftElbowIndex].z) < -0.2f)
                                {
                                    SetGestureJoint(ref gestureData, timestamp, rightHandIndex, jointsPos[rightHandIndex]);
                                    gestureData.progress = 0.5f;
                                }
                                break;

                            case 1:  // gesture phase 2 = complete
                                if ((timestamp - gestureData.timestamp) < 1.5f)
                                {
                                    bool isInPose = gestureData.joint == rightHandIndex ?
                                        jointsTracked[rightHandIndex] && jointsTracked[leftElbowIndex] && jointsTracked[rightShoulderIndex] &&
                                        (jointsPos[rightHandIndex].y - jointsPos[leftElbowIndex].y) > -0.1f &&
                                        Mathf.Abs(jointsPos[rightHandIndex].x - gestureData.jointPos.x) < 0.2f &&
                                        (jointsPos[rightHandIndex].z - gestureData.jointPos.z) < -0.1f :
                                        jointsTracked[leftHandIndex] && jointsTracked[rightElbowIndex] && jointsTracked[leftShoulderIndex] &&
                                        (jointsPos[leftHandIndex].y - jointsPos[rightElbowIndex].y) > -0.1f &&
                                        Mathf.Abs(jointsPos[leftHandIndex].x - gestureData.jointPos.x) < 0.2f &&
                                        (jointsPos[leftHandIndex].z - gestureData.jointPos.z) < -0.1f;

                                    if (isInPose)
                                    {
                                        Vector3 jointPos = jointsPos[gestureData.joint];
                                        CheckPoseComplete(ref gestureData, timestamp, jointPos, isInPose, 0f);
                                    }
                                }
                                else
                                {
                                    // cancel the gesture
                                    SetGestureCancelled(ref gestureData);
                                }
                                break;
                        }
                        break;


            case Gestures.OiZuki:
						switch(gestureData.state)
					  {
					    case 0:  // gesture detection - phase 1
					//						if(jointsTracked[leftHandIndex] && jointsTracked[leftElbowIndex] &&
					//					            (jointsPos[leftHandIndex].y - jointsPos[leftElbowIndex].y) > -0.05f &&
					//					            (jointsPos[leftHandIndex].x - jointsPos[leftElbowIndex].x) < 0f)
					//						{
					//							SetGestureJoint(ref gestureData, timestamp, leftHandIndex, jointsPos[leftHandIndex]);
					//							gestureData.progress = 0.5f;
					//						}

					      if(jointsTracked[leftHandIndex] && jointsTracked[hipCenterIndex] && jointsTracked[shoulderCenterIndex] && jointsTracked[leftHipIndex] && jointsTracked[rightHipIndex] &&
					          jointsPos[leftHandIndex].y >= gestureBottom && jointsPos[leftHandIndex].y <= gestureTop &&
					          jointsPos[leftHandIndex].x >= gestureLeft && jointsPos[leftHandIndex].x < gestureRight)
					      {
					        SetGestureJoint(ref gestureData, timestamp, leftHandIndex, jointsPos[leftHandIndex]);
					        gestureData.progress = 0.1f;
					      }
					      break;

					    case 1:  // gesture phase 2 = complete
					      if((timestamp - gestureData.timestamp) < 1.5f) //1.5 segundos para realizar movimiento
					      {
					//							bool isInPose = jointsTracked[leftHandIndex] && jointsTracked[leftElbowIndex] &&
					//								Mathf.Abs(jointsPos[leftHandIndex].y - jointsPos[leftElbowIndex].y) < 0.1f &&
					//								Mathf.Abs(jointsPos[leftHandIndex].y - gestureData.jointPos.y) < 0.08f &&
					//								(jointsPos[leftHandIndex].x - gestureData.jointPos.x) > 0.15f;

					        bool isInPose = jointsTracked[leftHandIndex] && jointsTracked[hipCenterIndex] && jointsTracked[shoulderCenterIndex] && jointsTracked[leftHipIndex] && jointsTracked[rightHipIndex] &&
					            jointsPos[leftHandIndex].y >= gestureBottom && jointsPos[leftHandIndex].y <= gestureTop &&
					            jointsPos[leftHandIndex].x > gestureRight;

					        if(isInPose)
					        {
					          Vector3 jointPos = jointsPos[gestureData.joint];
					          CheckPoseComplete(ref gestureData, timestamp, jointPos, isInPose, 0f);
					        }
					        else if(jointsPos[leftHandIndex].x >= gestureLeft)
					        {
					          float gestureSize = gestureRight - gestureLeft;
					          gestureData.progress = gestureSize > 0.01f ? (jointsPos[leftHandIndex].x - gestureLeft) / gestureSize : 0f;
					        }
					      }
					      else
					      {
					        // cancel the gesture
					        SetGestureCancelled(ref gestureData);
					      }
					      break;
					  }
					  break;

						case Gestures.Uke:
						switch(gestureData.state)
					  {
					    case 0:  // gesture detection - phase 1
					//						if(jointsTracked[rightHandIndex] && jointsTracked[rightElbowIndex] &&
					//					       (jointsPos[rightHandIndex].y - jointsPos[rightElbowIndex].y) > -0.05f &&
					//					       (jointsPos[rightHandIndex].x - jointsPos[rightElbowIndex].x) > 0f)
					//						{
					//							SetGestureJoint(ref gestureData, timestamp, rightHandIndex, jointsPos[rightHandIndex]);
					//							gestureData.progress = 0.5f;
					//						}

					      if(jointsTracked[rightHandIndex] && jointsTracked[hipCenterIndex] && jointsTracked[shoulderCenterIndex] && jointsTracked[leftHipIndex] && jointsTracked[rightHipIndex] &&
					        jointsPos[rightHandIndex].y >= gestureBottom && jointsPos[rightHandIndex].y <= gestureTop &&
					          jointsPos[rightHandIndex].x <= gestureRight && jointsPos[rightHandIndex].x > gestureLeft)
					      {
					        SetGestureJoint(ref gestureData, timestamp, rightHandIndex, jointsPos[rightHandIndex]);
					        gestureData.progress = 0.1f;
					      }
					      break;

					    case 1:  // gesture phase 2 = complete
					      if((timestamp - gestureData.timestamp) < 1.5f)
					      {
					//							bool isInPose = jointsTracked[rightHandIndex] && jointsTracked[rightElbowIndex] &&
					//								Mathf.Abs(jointsPos[rightHandIndex].y - jointsPos[rightElbowIndex].y) < 0.1f &&
					//								Mathf.Abs(jointsPos[rightHandIndex].y - gestureData.jointPos.y) < 0.08f &&
					//								(jointsPos[rightHandIndex].x - gestureData.jointPos.x) < -0.15f;

					        bool isInPose = jointsTracked[rightHandIndex] && jointsTracked[hipCenterIndex] && jointsTracked[shoulderCenterIndex] && jointsTracked[leftHipIndex] && jointsTracked[rightHipIndex] &&
					            jointsPos[rightHandIndex].y >= gestureBottom && jointsPos[rightHandIndex].y <= gestureTop &&
					            jointsPos[rightHandIndex].x < gestureLeft;

					        if(isInPose)
					        {
					          Vector3 jointPos = jointsPos[gestureData.joint];
					          CheckPoseComplete(ref gestureData, timestamp, jointPos, isInPose, 0f);
					        }
					        else if(jointsPos[rightHandIndex].x <= gestureRight)
					        {
					          float gestureSize = gestureRight - gestureLeft;
					          gestureData.progress = gestureSize > 0.01f ? (gestureRight - jointsPos[rightHandIndex].x) / gestureSize : 0f;
					        }
					      }
					      else
					      {
					        // cancel the gesture
					        SetGestureCancelled(ref gestureData);
					      }
					      break;
					  }
					  break;

						case Gestures.AgeUke:
						switch(gestureData.state)
						{
							case 0:  // gesture detection - phase 1
								if(jointsTracked[rightHandIndex] && jointsTracked[rightElbowIndex] &&
										 (jointsPos[rightHandIndex].y - jointsPos[rightElbowIndex].y) > 0.1f &&
										 (jointsPos[rightHandIndex].x - jointsPos[rightElbowIndex].x) > 0.05f)
								{
									SetGestureJoint(ref gestureData, timestamp, rightHandIndex, jointsPos[rightHandIndex]);
									gestureData.progress = 0.3f;
								}
								else if(jointsTracked[leftHandIndex] && jointsTracked[leftElbowIndex] &&
													(jointsPos[leftHandIndex].y - jointsPos[leftElbowIndex].y) > 0.1f &&
													(jointsPos[leftHandIndex].x - jointsPos[leftElbowIndex].x) < -0.05f)
								{
									SetGestureJoint(ref gestureData, timestamp, leftHandIndex, jointsPos[leftHandIndex]);
									gestureData.progress = 0.3f;
								}
								break;

							case 1:  // gesture - phase 2
								if((timestamp - gestureData.timestamp) < 1.5f)
								{
									bool isInPose = gestureData.joint == rightHandIndex ?
										jointsTracked[rightHandIndex] && jointsTracked[rightElbowIndex] &&
										(jointsPos[rightHandIndex].y - jointsPos[rightElbowIndex].y) > 0.1f &&
										(jointsPos[rightHandIndex].x - jointsPos[rightElbowIndex].x) < -0.05f :
										jointsTracked[leftHandIndex] && jointsTracked[leftElbowIndex] &&
										(jointsPos[leftHandIndex].y - jointsPos[leftElbowIndex].y) > 0.1f &&
										(jointsPos[leftHandIndex].x - jointsPos[leftElbowIndex].x) > 0.05f;

									if(isInPose)
									{
										gestureData.timestamp = timestamp;
										gestureData.state++;
										gestureData.progress = 0.7f;
									}
								}
								else
								{
									// cancel the gesture
									SetGestureCancelled(ref gestureData);
								}
								break;

							case 2:  // gesture phase 3 = complete
								if((timestamp - gestureData.timestamp) < 1.5f)
								{
									bool isInPose = gestureData.joint == rightHandIndex ?
										jointsTracked[rightHandIndex] && jointsTracked[rightElbowIndex] &&
										(jointsPos[rightHandIndex].y - jointsPos[rightElbowIndex].y) > 0.1f &&
										(jointsPos[rightHandIndex].x - jointsPos[rightElbowIndex].x) > 0.05f :
										jointsTracked[leftHandIndex] && jointsTracked[leftElbowIndex] &&
										(jointsPos[leftHandIndex].y - jointsPos[leftElbowIndex].y) > 0.1f &&
										(jointsPos[leftHandIndex].x - jointsPos[leftElbowIndex].x) < -0.05f;

									if(isInPose)
									{
										Vector3 jointPos = jointsPos[gestureData.joint];
										CheckPoseComplete(ref gestureData, timestamp, jointPos, isInPose, 0f);
									}
								}
								else
								{
									// cancel the gesture
									SetGestureCancelled(ref gestureData);
								}
								break;
						}
						break;
		}
	}
}
