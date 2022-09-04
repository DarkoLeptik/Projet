# Projet

# Update Avancée 03/09-11h26

# Actuellement, dans le projet il manque le ctrl+Z pour enlever les derniers changements ainsi que le reset de position sur le clic droit. Hormis ceci, je crois n'avoir rien oublié d'autre pour le moment.  Ce n'est pas la version finale néanmoins.

# Pour le reset du clic, j'ai essayé d'initialiser une variable _initialGizmos que l'on instancie au début mais aussi quand on supprime un gizmo pour que le _initialGizmos ait la bonne taille de tableau, mais cela ne fonctione pas.

# Pour ce qui est du ctrl+z, j'ai pensé dans le update à récupérer les inputs ctrl + z pour remettre une valeur précédente enregistrée mais je suis quasiment sûr que ce n'est pas la bonne méthode et je ne sais même pas comment bien l'implémenter.

# J'ai modifié un peu l'UI de l'EditorWindow pour se rapprocher de la fidélité du modèle.

# LAST UPDATE

# J'ai réessayé plusieurs choses pour le ctrl+z comme des Undo/Redo et des EditorGUI.BeginChangeCheck et EditorGUI.EndChangeCheck mais je ne suis arrivé à rien de concluant. Ainsi, pour le livrable final, il me manque comme vendredi après-midi, le ctrl+z et le reset de position avec le clic droit.