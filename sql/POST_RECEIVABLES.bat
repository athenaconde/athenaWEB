@echo "posting receivables ..."
cd/
cd "Program Files (x86)"
cd ibm
cd sqllib
cd bin
DB2 -tvf "D:\.NET Projects\Account Receivables\sql\post_receivables.sql"
@echo "Finish posting receivables!"