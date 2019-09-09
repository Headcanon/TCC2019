void FixedUpdate() {
        vineSpeed -= vineAng*0.09f;
        vineSpeed *= 0.992f;
        if (Time.time > vineStartTime+0.5f) {
            if (Mathf.Abs(xbox.LeftStickY) > 0.55f) {
                runSpeed = Mathf.Lerp(runSpeed, 0f, 0.4f);
                climbSpeed = Mathf.Clamp(climbSpeed + Mathf.Sign(xbox.LeftStickY) * 0.1f, -1f, 1f);
                if (climbSpeed > 0f) {
                    vineDist = Mathf.Clamp(vineDist - climbSpeed * Time.deltaTime, 2f, myVine.height);
                } else {
                    vineDist = Mathf.Clamp(vineDist - climbSpeed * Time.deltaTime * 2f, 2f, myVine.height);
                }
            } else {
                climbSpeed = Mathf.Lerp(climbSpeed, 0f, 0.4f);
                if (Mathf.Abs(xbox.LeftStickX) > deadzone) {
                    vineSpeed += xbox.LeftStickX;
                }
                runSpeed = vineSpeed / 70f * df;
            }
        } else {
            runSpeed = 0f;
            climbSpeed = 0f;
        }
        
        vineAng += vineSpeed * Time.deltaTime;

        float trang = (180f-vineAng)*Mathf.Deg2Rad;
        transform.position = new Vector3(myVine.transform.position.x+Mathf.Sin(trang)*vineDist,
            myVine.transform.position.y + Mathf.Cos(trang) * vineDist, transform.position.z);
        transform.localEulerAngles = new Vector3(0f, 0f, vineAng);

        myVine.taut = vineDist-1f;
        myVine.ang = vineAng;

        if (xbox.Action1.WasPressed) {
            transform.localEulerAngles = new Vector3(0f, 0f, 0f);

            rb.simulated = true;
            state = State.Norm;
            canReallyFlip = true;
            canFlip = false;
            if (Mathf.Abs(xbox.LeftStickX) > deadzone) df = Mathf.Sign(xbox.LeftStickX);
            vel = new Vector2(df * 12f, 12f);
            anim.Play("jumpForward");
            grounded = false;
            lastTimeOnGround = Time.time - 0.2f;
            vineStartTime = Time.time;
            canJumpCancel = false;
            canLedgeMantle = true;
            crouching = false;
            myVine.boarded = false;
            myVine = null;
        }
    }