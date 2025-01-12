# Changelog

All notable changes to this project will be documented in this file.

## In Progress

## [1.4.0] 2024-06-12

* Removed `Moq` dependency for tests.
* Updated project version to 20202.2.16f1 and added automated tests for
    backwards compatibility for versions 2019.4, 2020.3, 2021.3
* Removed reduction in momentum due to snapping up as it was causing player to
    sometimes get stuck on corners of small slopes.
* Updated project version to [2021.3.25f1](https://unity.com/releases/editor/whats-new/2021.3.25)
* Corrected basic overlap for when feet intersect with a wall or ramp.
* Added support for `LayerMask` for the `HumanoidFootIK` raycast checks
* Created class `IRaycastHelper` for easily mocking raycast checks.
* Overhauled existing animations to play well with foot ik and fixed import
    errors due to lazy coding
* Re-imported and created new animations so they are configured for Michelle's avatar
* Created `HumanoidFootIK` and `FootTarget` classes to manage basic player foot IK
* Created `HumanoidFootIKEditor` to bake animation curves for player movement
    when I'm too lazy to do it manually. Then proceeded to
    manually adjusted curves for player foot IK
* Added tests to verify basic behavior of `HumanoidFootIK` and `FootTarget` classes
* Updated avatar configurations and settings to support `HumanoidFootIK` usage
    for basic test scene
* Added more test objects in the `SampleScene` to verify new foot behaviors.
* Addressing small bug in player movement caused by stair movement normals
    not being computed properly when walking down stairs.

## [1.3.4] 2023-04-01

* Refactored common assets (like UI) to `ExampleFirstPersonKCC` sample 
* Added new type of `IColliderCast` in `PrimitiveColliderCast` which can select
    between different parameters via the `ColliderConfiguration` which includes
    a property drawer and custom editor for making it easier to
    configure and see changes reflected in the game.
* Small patch to accessability modifiers for `CameraConfig.CameraDistance`
* Added parameters to `IManagedCamera`:
    `CameraBase` and `PlayerBase`
* Small updates to DrawKCCBounces to improve the debug view.
* Small fix to `BufferedInput` to allow for resetting cooldown timer.
* Updated `KCCMovementEngine` to support layer masks for deciding what
    the player can and cannot collide with.

    _Note_ this may cause problems with existing instances of the `KCCMovementEngine`
    so I included an updated configuration to serialize a version number
    for the `KCCMovementEngine`, if this serialized version number is not set,
    it will update the `KCCMovementEngine` to use a default value of
    colliding with everything which was the previous behaviour.

* Updated the `IColliderCast` and `KCCUtils` apis to support passing
    an optional `LayerMask` and `QueryTriggerInteraction` parameters.

## [1.3.2] 2023-2-14

* Changed `RelativeParentConfig` to be a class to better persist information
    between updates instead of having data reset improperly.

## [1.3.1] 2023-2-13

* Adjusted how stairs and snap down are handled in `KCCMovementEngine`
    and `KCCUtils` to make player slowly move up or down stairs based on
    the remaining movement speed of the player.
* Adjusted `KCCMovementEngine` to have a max speed for snapping down
    to stop the player from teleporting down harshly and making player camera
    jitter.
* Added `IOnPlayerTeleport` to listen to teleport events from
    the `KCCMovementEngine` and respond accordingly.

## [1.3.0] 2023-1-29

_**Note**_ : This update will not automatically
copy values for `KCCStateMachine` from the previous
API version to the current API because they are
not compatible and have a simplified resource
values now. Sorry for any inconvenience this causes.
The latest [Usage](https://nickmaltbie.com/OpenKCC/docs/manual/usage.html)
notes should provide any necessary information
on how to update your parameters as needed.

* Added test coverage for `KCCMovementEngine` and refactored code.
* Removed some unused functions and properties from `KCCMovementEngine`,
    `KCCUtils`, and `IKCCConfig`.
* Added some tests for the untested behaviors.
* Refactored existing code to remove the `HumanoidKCCConfig` and moved
    most functionality to the `KCCMovementEngine`.
* Removed velocity from the `KCCMovementEngine` and manage this variable
    in the higher level `KCCStateMachine` or any other consumer
    of the movement engine features.
* Refactored the `IKCCConfig` to remove any values not directly
    used by the KCCUtils.
* Disabled player pushing and will add into back into the `KCCMovementEngine`
    at some future point if required.
* Simplified parameters for the `KCCStateMachine` to be easier to understand
    for an end user who doesn't want to configure all these values.
* Refactored debug draw code for character controller.
* Updated APIs available in the KCCMovementEngine to allow
    for passing a movement in world space to move the player.
* Changed KCCMovementEngine to get the configuration values
    from the KCCConfig and not from a MovementSettingsAttribute
* Depreciated old fields in the MovementSettingsAttribute
    that will be removed in a future update.

## [1.2.0] 2023-1-15

* Added Mole character sample.
* Added a new `KCCMovementEngine` to manage calls to `KCCUtils` via
    another layer of abstraction to avoid having to duplicate
    lots of code for player movement.
* Refactored `KCCStateMachine` to use the newly added `KCCMovementEngine`.

## [1.1.3] 2023-1-1

* Refactored code to use com.nickmaltbie.recolorshaderunity
* Fixed code reference to IEvent for backwards compatibility.

## [1.1.2] 2022-12-31

* Fixed materials in samples to be included in the package folder.

## [1.1.0] 2022-12-18

* Updated kcc state machine to be synced with the network kcc.
* Removed ParentConstraint requirement from the KCCStateMachine
    and replaced with a new `RelativeParentConfig` object to managed
    player's relative position to a parent object.
* Reorganized camera controller code to be more reusable between the main
    and netcode projects.
* Moved camera control code to the `nickmaltbie.OpenKCC.CameraControls`
    namespace within the openkcc project.
* Added example using unity Netcode and sub package com.nickmaltbie.openkcc.netcode
* Reformatted some code in the KCCStateMachine to reduce the amount of copied
    code between netcode character controller and basic character controller.
* Modified github actions workflows to deploy netcode example to the site
    under the directory Netcode.
* Depreciated old fields for objects that were duplicated in
    the netcode version of the openkcc.
    Whenever possible, I will include an auto-translation to the new format
    to avoid having to save and manually copy values between version upgrades.
    * Depreciated the old fields for KCCStateMachine and replaced with the
        object HumanoidKCCConfig. Also added some code to auto-convert the old
        fields into a HumanoidKCCConfig when the object is first de-serialized.
        Tagged this version with a string property and called this first version
        v1.0.0 and will use a semantic versioning system to update the value.
    * Depreciated the old fields for CameraController and replaced with the
        object CameraConfig. Also added some code to auto-convert th e old
        fields into a CameraConfig when the object is first de-serialized.
        Tagged this version with a string property and called this first version
        v1.0.0 and will use a semantic versioning system to update the value.

## [1.0.1] 2022-11-22

* Fixed import errors of project due to improperly setup test assembly definitions.

## [1.0.0] 2022-11-12

* Added a SprintingState to the KCCStateMachine.
* Updated the usage doc to include proper notes and links for KCCStateMachine.
* Switched to using com.nickmaltbie.StateMachineUnity library.
* Added Finite State Machine design docs and a decorator
    based code implementation to the project.
* Added KCCStateMachine implementation and changed relevant examples to use
    this state machine.
* Updated follow object to use unity's ParentConstraint
    examples for following parent objects.
* Updated unity version of project to v2021.3.11f1
* Adding test coverage for EditMode tests.

## [0.1.2] - 2022-06-26

Small patch to automated release workflow for npm

## [0.1.1] - 2022-06-21

Bumping project version to validate autoamted npm release workflow.

## [0.1.0] - 2022-06-19

Major Refactor

* Converting project to follow unity package layout
* Moved example FPS cahracter into example first person cahracter sample folder
* Moved simplified demo examples to demo sample folder
    * Also fixed dependency issues so each sample can be imported independently
* Setup package build workflow to copy `Assets/Samples` to
    `./Packages/com.nickmaltbie.openkcc/Samples~` and validating github workflow
    to ensure this works as expected.
* Created a common folder for assets shared between mulitple samples
    `./Packages/com.nickmaltbie.openkcc/Common` - will keep small files
    and assets here that are shared (but avoid large models and textures).
* Emptied all assets from `Assets\OpenKCC` to avoid complexity.

Minor Fixes

* Improved setup package script to fix an error with `git-lfs` files.
* Updated project dependencies definitions and install instructions.

Documentation

* Added notes on how to install the project via npm js registry

## [0.0.63] - 2022-06-10

Start of changelog.
