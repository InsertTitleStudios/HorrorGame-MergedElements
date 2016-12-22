#pragma strict

 public var distance : float = 2.0f;
 public var theAngle : int = 45;
 public var segments : int = 10;
 public var enemyHit : boolean = false;
 
 function Update() 
 {
     
         RaycastSweep();
     
 }
 
 function RaycastSweep() 
 {
     var startPos : Vector3 = transform.position; // umm, start position !
     var targetPos : Vector3 = Vector3.zero; // variable for calculated end position
     
     var startAngle : int = parseInt( -theAngle * 0.5 ); // half the angle to the Left of the forward
     var finishAngle : int = parseInt( theAngle * 0.5 ); // half the angle to the Right of the forward
     
     // the gap between each ray (increment)
     var inc : int = parseInt( theAngle / segments );
     
     var hit : RaycastHit;
     
     // step through and find each target point
     for ( var i : int = startAngle; i < finishAngle; i += inc ) // Angle from forward
     {
         targetPos = (Quaternion.Euler( 0, i, 0 ) * transform.forward ).normalized * distance;    
         
         // linecast between points
         if ( Physics.Raycast( startPos, targetPos, hit ) )
     {
         //   Debug.Log( "Hit " + hit.collider.gameObject.name );
             if (hit.collider.tag == "Small Enemy")
     {
        Debug.Log("Enemy");
        enemyHit = true;
     }
}    
else if (hit.collider.tag == "Wall") 
 {
     enemyHit = false;
 }
         
         // to show ray just for testing
         Debug.DrawRay( startPos, targetPos, Color.green );    
     }        
 }