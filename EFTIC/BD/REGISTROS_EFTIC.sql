-- Usar la base de datos EFTIC
USE EFTIC;
GO
-- Ingresar datos a la tabla "Sede"
INSERT INTO Sede (Nombre_Sede , Descripcion) 
VALUES
('PALACIO MUNICIPAL','Es el palacio municipal del Distrito de Pocollay , donde se Dijere el distrito , Esta ubicado al costado de la plaza central de Pocollay.'),
('SEGURIDAD CIUDADANA','Esta Sede se enfoca en la seguridad de los ciudadanos de Pocollay , esta ubicado a una cuadra de la plaza de Pocollay y a unos metros del Polideportivo de Pocollay.'),
('POLIDEPORTIVO','Polideportivo de Pocollay se caracteriza donde se practican los deportes del distrito , se hace como torneos de Vóley , Basquetbol entre otros eventos que le pidan a la municipalidad de Pocollay.'),
('CASA CULTURAL','Esta sede esta ubicado al costado del bando de la nación de la plaza de Pocollay , esta sede se caracteriza en apoyo de bonos a la población de Pocollay , como , Camastrones , Apoyo Económico a adultos mayores y mal de salud.'),
('AGRICULTURAL','Esta sede se caracteriza en coordinar en la área de lo agricultura de la población de Pocollay , para dar beneficios a los agricultores de Pocollay del Distrito de Pocollay.');

-- Ingresar datos a la tabla "Area"
INSERT INTO Area (Nombre_Area, Descripcion_Area, SedeID)
VALUES
('SECRETARÍA DE ALCALDÍA', 'Responsable de la oficina del alcalde', 1),
('OFICINA PROCURADURÍA PÚBLICA MUNICIPAL', 'Área encargada de la defensa jurídica del municipio', 1),
('OFICINA DE CONTROL INSTITUCIONAL', 'Control interno y auditorías del municipio', 1),
('UNIDAD DE SECRETARÍA GENERAL E IMAGEN', 'Encargada de la imagen institucional y la secretaria general', 1),
('EQUIPO FUNCIONAL DE REGISTRO CIVIL', 'Responsable del registro de nacimientos, matrimonios y defunciones', 1),
('EQUIPO FUNCIONAL DE IMAGEN INSTITUCIONAL', 'Responsable de la imagen y comunicación del municipio', 1),
('MÓDULO DE ATENCIÓN', 'Atención al público en general', 1),
('MESA DE PARTES', 'Recepción y derivación de documentos', 1),
('PORTERÍA DE INGRESO DE PERSONAL', 'Control de ingreso y salida de personal', 1),
('EQUIPO FUNCIONAL DE ARCHIVO CENTRAL', 'Manejo y archivo de documentos del municipio', 1),
('GERENCIA MUNICIPAL', 'Coordinación general de todas las áreas municipales', 1),
('SECRETARÍA DE GERENCIA MUNICIPAL', 'Asistencia directa a la gerencia municipal', 1),
('GERENCIA DE ADMINISTRACIÓN Y FINANZAS', 'Administración y finanzas del municipio', 1),
('SUB GERENCIA DE TESORERÍA', 'Manejo de los fondos públicos', 1),
('SUB GERENCIA DE RECURSOS HUMANOS', 'Gestión del personal municipal', 1),
('SUB GERENCIA DE ABASTECIMIENTOS', 'Control y gestión de los abastecimientos del municipio', 1),
('SUB GERENCIA DE BIENES PATRIMONIALES', 'Control de los bienes patrimoniales del municipio', 1),
('SUB GERENCIA DE CONTABILIDAD', 'Llevar la contabilidad del municipio', 1),
('GERENCIA DE ADMINISTRACIÓN TRIBUTARIA', 'Gestión de los tributos municipales', 1),
('SUB GERENCIA DE EJECUTORÍA COACTIVA', 'Encargada de la ejecución coactiva de deudas tributarias', 1),
('GERENCIA DE DESARROLLO URBANO E INFRAESTRUCTURA', 'Planeamiento urbano y desarrollo de infraestructura', 1),
('SUBGERENCIA DE ESTUDIOS DE INVERSIONES', 'Estudios y propuestas para inversión pública', 1),
('SUBGERENCIA DE PLANEAMIENTO URBANO Y CATASTRO', 'Planeamiento urbano y registro catastral', 1),
('GERENCIA DE ASESORÍA JURÍDICA', 'Asesoría legal al municipio', 1),
('GERENCIA DE PLANEAMIENTO, PRESUPUESTO Y DESARROLLO ORGANIZACIONAL', 'Planificación estratégica y desarrollo organizacional', 1),
('EQUIPO FUNCIONAL DE TECNOLOGÍAS DE LA INFORMACIÓN Y COMUNICACIONES', 'Encargado de la tecnología y comunicaciones del municipio', 3),
('GERENCIA DE DESARROLLO SOCIAL Y ECONÓMICO', 'Promoción del desarrollo social y económico del distrito', 4),
('SUBGERENCIA DE GESTIÓN AMBIENTAL Y MANTENIMIENTO', 'Encargado de la gestión ambiental y mantenimiento de espacios', 5),
('ULE SISFOH', 'Unidad Local de Empadronamiento de los hogares', 4),
('DEMUNA', 'Defensa de los derechos del niño y adolescente', 4),
('OMAPED - CIAM', 'Oficina de atención a personas con discapacidad y adultos mayores', 4),
('ALMACÉN CENTRAL', 'Manejo de los suministros del municipio', 3),
('SUBGERENCIA DE SERENAZGO MUNICIPAL', 'Coordinación de la seguridad ciudadana y serenazgo', 2),
('SERVICIO CEMENTERIO MUNICIPAL', 'Gestión y mantenimiento del cementerio municipal', 2),
('SERVICIO EQUIPO MECÁNICO', 'Mantenimiento y reparación de los vehículos municipales', 3),
('SUBGERENCIA DE DESARROLLO ECONÓMICO Y TURISMO', 'Promoción del turismo y desarrollo económico del distrito', 4),
('SERVICIO GRIFO MUNICIPAL', 'Administración del grifo municipal', 3),
('SEGURIDAD CIUDADANA (TELEFONO DIRECTO)', 'Teléfono de contacto directo para emergencias de seguridad ciudadana', 2);


-- Ingresar datos a la tabla "Usurio"
INSERT INTO Usuario(DNI , Nombre_Usuario , Apellido_Usuario , Correo, Password,TipoUsuario,SedeID,AreaID) 
VALUES
('77235530','JESUS LEONEL','CASTRO GUTIERREZ','jescastro@upt.pe','77235530','SOPORTE',1,26);

-- Ingresar datos a la tabla "Tipo Equipo"
INSERT INTO Tipo_Equipo( Nombre_Tipo_Equipo , Descripcion_Tipo_Equipo ) 
VALUES
('CPU','Computadora del usuario para que pueda laboral su Trabajo en la municipalidad'),
('MONITOR','Pantalla de la computadora del usuario'),
('IMPRESORA','Artefacto donde puede imprimir toda su documentación'),
('FUENTE DE PODER','La caja de electricidad para la computadora'),
('TECLADO','Periférico de la computadora'),
('MOUSE','Periférico de la computadora'),
('DISCO DURO','Almacenamiento de la computadora donde guardan todos sus datos');


-- Insertar registros en la tabla Fallas
INSERT INTO Falla(Nombre_Falla, Descripcion_Falla) 
VALUES 
('NO FUNCIONA', 'El equipo no enciende o no responde a ningún comando.'),
('INFECCION VIRUS', 'El equipo está afectado por malware o virus.'),
('SIN SISTEMAS', 'El equipo carece de sistemas operativos o programas esenciales.'),
('NO IMPRIME', 'La impresora no realiza ninguna impresión a pesar de los intentos.'),
('LENTITUD', 'El equipo funciona de manera extremadamente lenta.'),
('FUNCION ERRATICA', 'El equipo tiene un comportamiento impredecible o errores frecuentes.'),
('SONIDO ANORMAL', 'El equipo emite sonidos inusuales o anormales.'),
('SIN INTERNET', 'El equipo no tiene conexión a internet.');


-- Insertar registros en la tabla Otras_actividades
INSERT INTO O_Actividades(Nombre_O_Actividad, Descripcion_O_Actividad) 
VALUES 
('DIAGNOSTICO', 'Evaluación del equipo para identificar fallas o problemas.'),
('MANTENIMIENTO', 'Realización de tareas preventivas o correctivas en el equipo.'),
('INSTALACION', 'Colocación e instalación de software o hardware.'),
('ESCANEO', 'Proceso de digitalización de documentos o búsqueda de virus.'),
('CONFIGURACION', 'Ajustes o modificaciones en la configuración del equipo o sistema.'),
('IMPRESION', 'Actividades relacionadas con la impresión de documentos.'),
('BACKUPS', 'Realización de copias de seguridad de los datos.'),
('CAPACITACION', 'Entrenamiento o formación a usuarios sobre el uso de sistemas o equipos.');
