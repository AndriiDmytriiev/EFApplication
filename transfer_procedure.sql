-- PROCEDURE: public.transfer(integer, integer, integer, integer)

-- DROP PROCEDURE IF EXISTS public.transfer(integer, integer, integer, integer);

CREATE OR REPLACE PROCEDURE public.transfer(
	IN i integer,
	IN from_account_id integer,
	IN to_account_id integer,
	IN amount integer)
LANGUAGE 'plpgsql'
AS $BODY$
  Begin
   IF  (amount >= 1)  THEN
	
        -- Update balances
		
           --INSERT INTO AUDIT(EMP_ID, ENTRY_DATE) VALUES (i, current_timestamp);
        UPDATE accounts SET balance = balance - amount WHERE id = from_account_id;
        UPDATE accounts SET balance = balance + amount WHERE id = to_account_id;

        -- Commit the transaction
          COMMIT;
	
    ELSE
	
        -- Rollback the transaction if there's not enough balance
        ROLLBACK;
        RAISE EXCEPTION 'Insufficient balance in the source account';

    END IF;
   
   End;
$BODY$;
ALTER PROCEDURE public.transfer(integer, integer, integer, integer)
    OWNER TO postgres;
	