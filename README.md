# Introducción

Este documento proporciona una guía detallada para comenzar con el desarrollo de avatares en VRChat utilizando el VRChat Creator Companion (VCC) y el SDK de OWO. La guía está diseñada para ayudar paso a paso para poder configurar de manera adecuada un avatar y subirlo a la plataforma de VRChat.

----

# Primeros pasos

1. [Descargar VRChat Creator Companion (VRChat SDK)](https://vrchat.com/download/vcc "Click aquí para descargar VRChat Creator Companion (VRChat SDK)")
2. [Descargar el package de OWO para VRChat](https://github.com/uzair-ashraf/vrc-owo-suit/releases/download/0.0.1/OWO_Suit.unitypackage "Descargar el package de OWO")
3. [Descargar Unity 2022.3.22f1 (Es necesario tener instalado Unity Hub)](unityhub://2022.3.22f1/887be4894c44 "Descargar Unity 2022.3.22f1 (Es necesario tener instalado Unity Hub)")
---
### VRCC Hub

Una vez descargado y configurado el SDK de VRChat, nos dejará acceder a su hub.

![Hub de Creator Companion](https://i.ibb.co/sqsbk6h/VRC.png "Hub de Creator Companion")

- <p style="color:red;">**Pestaña de proyectos:** En ella podemos agregar o crear nuevos proyectos con el SDK de VRC.</p>
- <p style="color:#888a36;">**Botón de crear un nuevo proyecto:** Al seleccionarlo, podrás crear un proyecto a través de distintas plantillas.</p>
- <p style="color:#21afcc;">**Botón de agregar un proyecto existente:** Nos permite buscar entre nuestros archivos para seleccionar un proyecto que ya tengamos y abrirlo con el SDK.</p>
- [Documentación completa de Creator Companion](https://vcc.docs.vrchat.com/ "Documentación completa de Creator Companion")

----
### Crear nuevo proyecto en VRCC Hub

![Nuevo proyecto en VRCC Hub](https://i.ibb.co/bRwcmXr/VRCNP.png "Nuevo proyecto en VRCC Hub")

1. Decide si deseas que sea un proyecto de avatar o de mundo.

2. Asigna un nombre.

3. Asegúrate de que la ubicación de guardado sea correcta.

4. Haz clic en **Crear Proyecto**.

----
### Abrir proyecto en VRCC Hub

Al seleccionar el botón de agregar un proyecto existente, se abrirá el explorador de archivos. Debes seleccionar la carpeta que contiene el proyecto que deseas agregar.

----
### Primeros pasos dentro del proyecto
Apenas estés dentro del proyecto, debes dirigirte a la barra de herramientas donde estará el nuevo apartado **VRChat SDK**, en él, debes seleccionar el panel de control.

![Barra SDK](https://i.ibb.co/998Cb6n/BarraSDK.png "Barra SDK")

Lo primero que debes hacer es iniciar sesión en tu cuenta de VRChat, es importante que esta cuenta tenga un rango mayor a **New User**
> Para subir de rango es necesario jugar un mínimo de 10-12 horas al juego de manera activa, no cuenta estar AFK.

![Primeros pasos con el SDK de VR Chat](https://i.ibb.co/FJr1dCD/Primeros-pasos-SDK.png "Primeros pasos con el SDK de VR Chat")

Una vez iniciada la sesión, nos indicará si estás habilitado para poder subir contenido a la plataforma.

![Sesión iniciada en el SDK](https://i.ibb.co/V35hMxX/sesion-iniciada.png "Sesión iniciada en el SDK")

---

### Configurar avatar

- Agrega el modelo a la escena.

![Agregar modelo a la escena de Unity](https://i.ibb.co/K61xPKJ/Agregar-modelo.png "Agregar modelo a la escena de Unity")

- Añade el componente de VRC Avatar Descriptor al modelo.

![Añadir componente](https://i.ibb.co/gJzHp45/Anadir-componente.png "Añadir componente")

![Componente agregado](https://i.ibb.co/Dk7jBcj/Componente.png "Componente agregado")

### Configuraciones necesarias

#### View

Acá puedes configurar el punto de vista del avatar, al editarlo habilita gizmo en el editor, lo ideal es colocarlo en donde estarían los ojos del personaje.

![View](https://i.ibb.co/MRpf18M/View.png "View")

#### Playable Layer

En este sub-menú solo debes configurar la Layer de FX para utilizar el SDK de OWO.

![Playable layer](https://i.ibb.co/8smvVVw/Playable.png "Playable layer")

Para eso, necesitas un Animator Controller que se asignará al FX.
> Para crearlo debes ir a Assets > Create > Animation Controller

![FX Layer](https://i.ibb.co/kczFDFY/FX.png "FX Layer")

#### Expressions

Acá debes agregar un menú de expresión y un menú de parámetros.

![](https://i.ibb.co/6sbPQJZ/parametro.png)

> Para crearlos debemos ir a Assets > Create > VRChat > Avatars

![](https://i.ibb.co/t39w3ST/parametro2.png)

###### Ahora puedes pasar al SDK de OWO.

----

### OWO SDK

Si ya importaste el package de OWO, debería aparecer en la barra superior una nueva opción llamada **Shadoki**.

![](https://i.ibb.co/56KPW8S/shadokio.png)

En este menú, debes asignar nuestro modelo que contiene el VRC Avatar Descriptor que configuramos anteriormente.

![](https://i.ibb.co/qD8dzSj/avcatar.png)

Una vez asignado, presiona **Add**.

![](https://i.ibb.co/0Y1j85V/add.png)

Se agregarán nuevos objetos dentro del modelo, los cuales debemos posicionar, rotar y escalar para hacerlo coincidir con cada parte del modelo, o un aproximado de donde estaría.

![](https://i.ibb.co/DCQ8D3y/owowo.png)

Una vez posicionado, movemos los objetos dentro de la jerarquía del editor para dejarlos dentro del hueso correspondiente.

![](https://i.ibb.co/Yfn1bTQ/bone.png)

Con esto listo, ya se puede subir el avatar a VRChat.

-----
### Subir avatar a VRChat

En la pestaña **Builder** del SDK de VRChat podemos configurar los últimos detalles para finalmente subir el avatar a la plataforma.

Debemos agregar:

- Nombre
- Descripción
- Miniatura

Con esto listo, podemos subirlo a VRChat de manera pública o privada. También podemos probarlo de manera offline.

![](https://i.ibb.co/L0vczPz/Subir-Avatar.png)

----
### Posibles errores
#### Solucionar error BluePrint ID

En caso de presentarse el error de BluePrint ID, debemos ir a nuestro modelo y, debajo del VRC Avatar Descriptor que configuramos en un principio, también se agregó automáticamente un Pipeline Manager. Simplemente presionamos *Detach* y ya debería funcionar sin problemas. En caso de no estar el botón *Detach*, agregamos un ID aleatorio y luego presionamos *Detach*.

![](https://i.ibb.co/1z51Gxm/error.png)
