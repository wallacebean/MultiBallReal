using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BepInEx;
using HarmonyLib;
using UnityEngine;
using LLHandlers;
using GameplayEntities;
using LLGUI;
using Abilities;
using System.Reflection;
using System.Reflection.Emit;
using BepInEx.Logging;
using BepInEx.Configuration;
using LLBML;
using LLBML.States;
using LLBML.Utils;
using LLBML.Math;
using LLBML.Players;
using LLBML.Networking;



namespace multiballreal
{
	[BepInPlugin("us.wallace.plugins.llb.multiballreal", "multiballreal Plug-In", "1.0.1.0")]
	[BepInDependency(LLBML.PluginInfos.PLUGIN_ID, BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("no.mrgentle.plugins.llb.modmenu", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInProcess("LLBlaze.exe")]

    public class Plugin : BaseUnityPlugin

    {
        public static ManualLogSource Log { get; private set; } = null;

		public static ConfigEntry<int> maxBalls;
		public static ConfigEntry<int> enableMultiBall;
		

		void Awake()
        {
			

			maxBalls = Config.Bind("1. multiBallConfig", "Number of Balls", 2, "Number of Balls");
			
			
			

			Log = this.Logger;
			
			Logger.LogDebug("Patching effects settings...");



            var harmony = new Harmony("us.wallace.plugins.llb.multiballreal");

			
			{
				harmony.PatchAll(typeof(BallHandlerSpawnBallPatch));
			}


			Logger.LogDebug("multiballreal is loaded");
			global::World.MAX_BALLS = Plugin.maxBalls.Value;
		}

		void Start()
		{
			ModDependenciesUtils.RegisterToModMenu(this.Info);
			
		}

		 void FixedUpdate()
		{
			global::World.MAX_BALLS = Plugin.maxBalls.Value;
		}


		}

		class BallHandlerSpawnBallPatch
	{
		[HarmonyPatch(typeof(BallHandler), nameof(BallHandler.SpawnBall))]
		[HarmonyPrefix]
		public static bool SpawnBall_Prefix(BallHandler __instance, int ballIndex = 0)
		{
			
			int num = global::LLHandlers.PlayerHandler.instance.playerHandlerData.firstLosingPlayerOfBurst;
			if (num != global::World.NO_PLAYER_INDEX && !global::ALDOKEMAOMB.BJDPHEHJJJK(num).NGLDMOLLPLK)
			{
				num = global::World.NO_PLAYER_INDEX;
			}
			global::LLHandlers.PlayerHandler.instance.playerHandlerData.firstLosingPlayerOfBurst = global::World.NO_PLAYER_INDEX;
			__instance.ballHandlerData.ballSpawnPos = new global::IBGCBLLKIHA(__instance.world.GetStageCenter().GCPKPHMKLBN, global::HHBCPNCDNDH.FCKBPDNEAOG(__instance.world.stageMax.CGJJEHPPOAN, global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(200.0m), global::World.FPIXEL_SIZE)));
			if (num != global::World.NO_PLAYER_INDEX)
			{
				if (global::JOMBNFKIHIC.GIGAKBJGFDI.PNJOKAICMNN == global::GameMode.STRIKERS || global::JOMBNFKIHIC.GIGAKBJGFDI.PNJOKAICMNN == global::GameMode.VOLLEYBALL)
				{
					if (global::LLHandlers.PlayerHandler.instance.GetPlayerEntity(num).playerData.team == global::BGHNEHPFHGC.EHPJJADIPNG)
					{
						__instance.ballHandlerData.ballSpawnPos.GCPKPHMKLBN = global::HHBCPNCDNDH.FCKBPDNEAOG(__instance.world.GetStageCenter().GCPKPHMKLBN, global::HHBCPNCDNDH.FCGOICMIBEA(__instance.world.stageSize.GCPKPHMKLBN, global::HHBCPNCDNDH.NKKIFJJEPOL(5)));
					}
					else
					{
						__instance.ballHandlerData.ballSpawnPos.GCPKPHMKLBN = global::HHBCPNCDNDH.GAFCIOAEGKD(__instance.world.GetStageCenter().GCPKPHMKLBN, global::HHBCPNCDNDH.FCGOICMIBEA(__instance.world.stageSize.GCPKPHMKLBN, global::HHBCPNCDNDH.NKKIFJJEPOL(5)));
					}
				}
				__instance.balls[ballIndex].SetToTeam(global::LLHandlers.PlayerHandler.instance.GetPlayerEntity(num).playerData.team);
			}
			else
			{
				__instance.ballHandlerData.ballSpawnPos = new global::IBGCBLLKIHA(__instance.world.GetStageCenter().GCPKPHMKLBN, global::HHBCPNCDNDH.FCKBPDNEAOG(__instance.world.stageMax.CGJJEHPPOAN, global::HHBCPNCDNDH.AJOCFFLIIIH(global::HHBCPNCDNDH.NKKIFJJEPOL(200.0m), global::World.FPIXEL_SIZE)));
				
				__instance.balls[ballIndex].SetToTeam(global::BGHNEHPFHGC.NMJDMHNMDNJ);
			}
			for (int i = ballIndex; i < global::World.MAX_BALLS; i++)
			{

				__instance.balls[i].Spawn(num);
				
			}
			return false;
			
		}
	}


}
