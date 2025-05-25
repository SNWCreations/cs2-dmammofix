# cs2-dmammofix

A plugin which refills the reserved ammo of the weapon players use.

This plugin is designed for [kus' modded server pack](https://github.com/kus/cs2-modded-server)

Due to technical limitation, currently we can't fully simulate `sv_infinite_ammo 2`,
 so we refill the reserved ammo when player shoots.

This issue will be solved if we figured out how to insert a callback to be called
 after a weapon got reloaded.

## License

Copyright (C) 2025 SNWCreations, Licensed under MIT License.
