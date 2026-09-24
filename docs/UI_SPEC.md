# FLuXmenu UI Specification

## Identity

FLuXmenu is monochrome by default.

- Background: black, recommended alpha 0.82-0.90
- Line/icon/text: white
- Hovered wedge: white fill
- Hovered content: black
- Disabled content: white at reduced opacity
- No required accent color

## Interaction

Default mode is **Hold → Aim/Flick → Release**.

1. Press and hold the FLuXmenu binding.
2. Menu spawns relative to the configured hand.
3. Moving outside the dead zone selects a wedge.
4. Crossing into a new wedge produces a short haptic tick.
5. Release activates the selected action.
6. Releasing inside the dead zone cancels/closes.
7. Page links replace the current wheel and keep the menu open.
8. Center/back input returns to the parent page.

## Segment count

Prefer 4-8 visible segments. Pages with more items should paginate or use nested categories. Avoid tiny wedges.

## Motion

Recommended open animation: 80-120 ms scale/fade.
Recommended hover scale: 1.04-1.08x.
Recommended close animation: <= 100 ms.
Reduced-motion mode should remove scale/rotation and keep only a short fade.

## Performance

- Reuse segment objects instead of destroying/recreating each frame.
- Cache text/icon materials.
- Do not update dynamic labels faster than needed.
- Avoid blur on Quest; use translucent black instead.
- Keep all icons simple monochrome masks.
