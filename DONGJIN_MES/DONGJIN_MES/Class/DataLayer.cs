using System;
using System.Data;
using Npgsql;
using Newtonsoft.Json;
using DONGJIN_MES.IUserControl;
using DONGJIN_MES.Class;
using Newtonsoft.Json;

namespace PLCMonitoring.Class
{
    public class DataLayer
    {
        //public MySqlConnection m_cnn;
        //public MySqlCommand m_cmd;
        public NpgsqlConnection m_cnn;
        //public DataLayer()
        //{
        //    m_cnn = new NpgsqlConnection(GlobalVariable.Connection);
        //}
        public DataLayer(string con)
        {
            m_cnn = new NpgsqlConnection(con);
            //m_cnn.Settings.Timezone = "+07";
        }
        public DataTable QueryDataPos(string query)
        {
            DataTable dt = new DataTable();
            try
            {
                m_cnn.Open();
                var cmd = new NpgsqlCommand(query, m_cnn);
                NpgsqlDataAdapter ad = new NpgsqlDataAdapter(cmd);
                m_cnn.Close();
                ad.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                m_cnn.Close();
                throw ex;
            }
        }
        public bool ExcuteQueryPos(string query)
        {
            bool rs = false;
            try
            {
                m_cnn.Open();
                using (NpgsqlCommand cmd = m_cnn.CreateCommand())
                {
                    cmd.CommandText = query;
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                    rs = true;
                }
                m_cnn.Close();
            }
            catch (Exception ex)
            {
                m_cnn.Close();
                throw ex;
            }
            return rs;
        }
        //public DataTable QueryData(string query)
        //{
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        m_cnn.Open();
        //        MySqlDataAdapter adapter = new MySqlDataAdapter(query, m_cnn);
        //        adapter.Fill(dt);
        //        m_cnn.Close();
        //        return dt;
        //    }catch(Exception ex)
        //    {
        //        m_cnn.Close();
        //        throw ex;
        //    }
        //}
        //public bool ExcuteQuery(string query)
        //{
        //    try
        //    {
        //        m_cnn.Open();
        //        m_cmd = new MySqlCommand(query,m_cnn);
        //        m_cmd.ExecuteNonQuery();
        //        m_cnn.Close();
        //        return true;
        //    }
        //    catch(Exception ex)
        //    {
        //        m_cnn.Close();
        //        throw ex;
        //    }
        //}

        internal DataTable UpdateVersion(string appname) => QueryDataPos($"select * from t_manage_app_version where i_app_name='{appname}' and i_yn='Y'");
        internal DataTable GetProdGRId(string po) => QueryDataPos($"select prod_gr_id from t_production_gr where prod_order_no='{po}' and i_state='RUNNING' and i_factory_code='{StaticSetting.FactoryCode}' order by prod_gr_id asc");
        internal DataTable GetTarget() => QueryDataPos($"select i_value from t_md_factory_configuration where i_code='ST00000001' and i_state='RUNNING'");
        internal DataTable GetStatus() => QueryDataPos($"select * from t_common_code where i_group_id='D019000'");
        internal DataTable GetPOType() => QueryDataPos($"select * from t_common_code where i_group_id='D020000'");
        internal DataTable GetLanguage() => QueryDataPos($"select * from t_md_production_language");
        internal DataTable CheckUser(string user, string pass) => QueryDataPos($"select * from user_table where i_state='RUNNING' and user_state='VALID' and email='{user}' and password='{pass}'");

        internal DataTable LoadScreen(string screen_id, string mac) => QueryDataPos($"select * from t_rm_prod_result_config where i_mac='{mac}' and i_screen_id='{screen_id}'");
        internal DataTable ProdTime() => QueryDataPos($"select * from t_md_factory_configuration where i_state='RUNNING' and i_factory_code='{StaticSetting.FactoryCode}' and i_code='RE00000001'");
        internal DataTable ProdStatus(string sta1,string sta2,string sta3,string po) => QueryDataPos($"select * from t_md_plan where i_factory_code='{StaticSetting.FactoryCode}' and i_prod_status in ('{sta1}','{sta2}','{sta3}') and i_prod_order_no='{po}'");
        internal DataTable LoadDaily(string line_code, string con, string ws) => QueryDataPos($"select a.i_process,a.i_version,a.i_state, a.i_plan_date as plan_date,b.i_code as line_code,b.i_name as line_name,a.i_prod_order_no,a.i_tact_time,a.i_plan_qty,a.i_actual_qty,c.i_material_id as model_code,c.i_name as model_name,b.i_code as vendor_code,a.i_factory_code,a.model_id,a.i_po_type,f.lot_no,g.prod_gr_id from t_md_plan a left join t_md_line b on b.line_id=a.line_id and b.i_factory_code =a.i_factory_code left join t_md_bom d on d.i_bom_id =a.model_id and d.i_factory_code =a.i_factory_code left join t_md_material_master c on c.i_material_master_id=d.material_parent_code_id left join t_production_label f on f.plan_id= a.i_id and f.i_factory_code=a.i_factory_code left join t_production_gr g on g.i_factory_code=a.i_factory_code and g.po_id=a.i_id left join t_md_process h on h.line_id=a.line_id left join t_md_work_station m on m.process_id=h.process_id left join t_common_code n on n.i_code=a.i_apr_status where b.i_code ='{line_code}' {con} and m.i_code='{ws}' and a.i_state='RUNNING' and a.i_factory_code='{StaticSetting.FactoryCode}' and n.i_code_name='APPROVED' order by a.i_prod_order_no asc");

        internal DataTable LoadActual(string line_code, string con, string ws) => QueryDataPos($"select sum(c.i_serial_qty) as qty,c.i_po from t_production_scan c left join t_md_plan a on a.i_prod_order_no =c.i_po left join t_md_line b on b.line_id =a.line_id where b.i_code ='{line_code}' {con} and c.i_state_serial ='RUNNING' and c.i_ws_code ='{ws}' and c.i_factory='{StaticSetting.FactoryCode}' group by c.i_po ");
        internal DataTable LoadLineInfo() => QueryDataPos($"select distinct(i_code) as line_code, i_name as line_name from t_md_line where i_factory_code='{StaticSetting.FactoryCode}' order by i_name asc");
        internal DataTable GetPOPLan(string serial,string processCode) => QueryDataPos($"select c.*,e.i_name,f.i_code,g.i_code as line_code,c.i_plan_date,c.i_prod_status from t_print_label a left join t_production_label b on b.production_label_id=a.production_label_id and b.i_factory_code=a.i_factory_code left join t_md_plan c on c.i_id=b.plan_id and c.i_factory_code=b.i_factory_code left join t_md_bom d on d.i_bom_id =c.model_id and d.i_factory_code =c.i_factory_code left join t_md_material_master e on e.i_material_master_id=d.material_parent_code_id and e.i_factory_code=a.i_factory_code left join t_md_process f on f.line_id=c.line_id left join t_md_line g on g.line_id=c.line_id where a.e_pass_no='{serial}' and a.i_state='RUNNING' and a.i_factory_code='{StaticSetting.FactoryCode}' and f.i_code='{processCode}'");
        internal DataTable GetReflect(string process,string ws) => QueryDataPos($"select a.final_yn,b.reflect from t_md_process a left join t_md_work_station b on b.i_factory_code=a.i_factory_code and b.process_id=a.process_id where a.i_code='{process}' and a.i_factory_code='{StaticSetting.FactoryCode}' and a.final_yn='Y' and b.reflect='Y' and b.i_code='{ws}'");

        internal DataTable GetReflectSorting(string process, string ws) => QueryDataPos($"select a.final_yn,b.reflect from t_md_process a left join t_md_work_station b on b.i_factory_code=a.i_factory_code and b.process_id=a.process_id where a.i_code='{process}' and a.i_factory_code='{StaticSetting.FactoryCode}' and b.i_code='{ws}'");
        internal DataTable GetProcess(string lineCode) => QueryDataPos($"select distinct(a.i_code) as process_code,a.prod_plan,c.i_code_name as process_name,a.final_yn,a.input_yn,a.barcode_yn from t_md_process a left join t_md_line b on b.line_id =a.line_id left join t_common_code c on substring(c.i_code,6,2)=substring(a.i_code,3,2) and c.i_state ='RUNNING' and a.name=c.i_code where b.i_code ='{lineCode}' and a.prod_plan='Y' and c.i_group_id ='D016000' and a.i_factory_code='{StaticSetting.FactoryCode}'");
        internal DataTable GetWS(string processCode) => QueryDataPos($"select a.i_code ,a.i_name,a.reflect from t_md_work_station a left join t_md_process b on b.process_id =a.process_id  where b.i_code ='{processCode}' and a.i_factory_code='{StaticSetting.FactoryCode}' order by a.i_name asc");

        internal DataTable CheckSerialExsit(string serial) => QueryDataPos($"select * from t_print_label where e_pass_no='{serial}' and i_factory_code='{StaticSetting.FactoryCode}'");
        internal DataTable CheckSerialScan(string serial, string ws) => QueryDataPos($"select * from t_production_scan where i_label_serial='{serial}' and i_state_serial='RUNNING' and i_ws_code='{ws}' and i_factory='{StaticSetting.FactoryCode}'");
        internal DataTable RePrint(string labelbox) => QueryDataPos($"select distinct(a.i_box_no) as label_box,b.qty,c.i_name as line_name,d.i_name as model_name,a.i_po  from t_production_scan a left join t_print_box_label b on b.box_no=a.i_box_no and b.i_factory_code=a.i_factory left join t_md_line c on c.i_code =a.i_line_code and c.i_factory_code=a.i_factory left join t_md_material_master d on d.i_material_id =a.i_model where a.i_box_no ='{labelbox}' and a.i_factory='{StaticSetting.FactoryCode}'");
        internal DataTable LoadSeqBox(string line, string po) => QueryDataPos($"select * from t_prd_print_history where i_ymd='{DateTime.Now.ToString("yyyy-MM-dd")}' and i_line_code='{line}' and i_po='{po}' and a.i_factory_code='{StaticSetting.FactoryCode}' order by i_created desc");
        internal DataTable SearchBoxFPO(string box) => QueryDataPos($"select sum(i_serial_qty) as qty,i_box_no from t_production_scan where {box} and i_factory='{StaticSetting.FactoryCode}' group by i_box_no order by i_box_no asc");
        internal DataTable SearchBox(string box) => QueryDataPos($"select sum(i_serial_qty) as qty,i_box_no from t_production_scan where {box} and i_factory='{StaticSetting.FactoryCode}' group by i_box_no order by i_box_no asc");
        internal DataTable SearchBoxDateTime(string box) => QueryDataPos($"select i_box_no,i_dte_created from t_production_scan where {box} and i_factory='{StaticSetting.FactoryCode}' order by i_dte_created desc");
        internal DataTable GetSerial(string po, string ws) => QueryDataPos($"select i_label_serial,i_dte_created from t_production_scan where i_po='{po}'  and i_state_serial ='RUNNING' and i_ws_code='{ws}' and (i_state <>'RUNNING' or i_state is null) and i_factory='{StaticSetting.FactoryCode}' order by i_dte_created desc");
        internal DataTable GetPS(string po, string ws) => QueryDataPos($"select sum(i_serial_qty) as qty from t_production_scan where i_po='{po}' and i_state_serial='RUNNING' and i_factory='{StaticSetting.FactoryCode}' and i_ws_code='{ws}' and (i_state is null or i_state='DELETED')");
        internal DataTable SearchBoxDateTimeOFPO(string box) => QueryDataPos($"select i_box_no,i_dte_created from t_production_scan where {box} and i_factory='{StaticSetting.FactoryCode}' order by i_dte_created desc");

        internal DataTable GetPOQty(string po,string ws) => QueryDataPos($"select sum(i_serial_qty) qty from t_production_scan where i_po='{po}' and i_factory='{StaticSetting.FactoryCode}' and i_state_serial='RUNNING' and i_ws_code='{ws}'");
        internal DataTable SearchBoxDetail(string box, string ws) => QueryDataPos($"select i_dte_created as dte,i_label_serial from t_production_scan where i_box_no='{box}' and i_ws_code='{ws}' and i_factory='{StaticSetting.FactoryCode}' and i_state_serial='RUNNING' order by i_label_serial asc");
        internal bool SaveSerial(SetInfo setInfo) => ExcuteQueryPos($"INSERT INTO t_production_scan (i_factory,i_ws_code,i_label_serial,i_po,i_model,i_line_code,i_dte_created,i_process_code,input_yn,final_yn,i_dte_log_i,i_dte_log_u,i_usr_log_i,i_usr_log_u,i_version,i_state_serial,i_reflect) VALUES ('{setInfo.Factory}','{setInfo.WSCode}','{setInfo.Serial}','{setInfo.PO}','{setInfo.Model}','{setInfo.Line}',NOW(),'{setInfo.ProcessCode}','{setInfo.Input}','{setInfo.Final}',NOW(),NOW(),'{UserSettings.Email}','{UserSettings.Email}','{setInfo.Verison}','RUNNING','{setInfo.Reflect}')  ON CONFLICT (i_label_serial,i_ws_code,i_factory) DO UPDATE SET i_dte_log_i = EXCLUDED.i_dte_log_i,i_dte_log_u = EXCLUDED.i_dte_log_u,i_usr_log_i = EXCLUDED.i_usr_log_i,i_usr_log_u = EXCLUDED.i_usr_log_u,i_state = EXCLUDED.i_state,i_state_serial = EXCLUDED.i_state_serial,i_reflect = EXCLUDED.i_reflect");
        internal bool SaveSerialInput(SetInfo setInfo) => ExcuteQueryPos($"INSERT INTO t_production_scan (i_factory,i_ws_code,i_label_serial,i_po,i_model,i_line_code,i_dte_created,i_process_code,input_yn,final_yn,i_dte_log_i,i_dte_log_u,i_usr_log_i,i_usr_log_u,i_version,i_state_serial,i_reflect,i_state,i_serial_qty,i_box_no) VALUES ('{setInfo.Factory}','{setInfo.WSCode}','{setInfo.LabelBox}','{setInfo.PO}','{setInfo.Model}','{setInfo.Line}',NOW(),'{setInfo.ProcessCode}','{setInfo.Input}','{setInfo.Final}',NOW(),NOW(),'{UserSettings.Email}','{UserSettings.Email}','{setInfo.Verison}','RUNNING','{setInfo.Reflect}','RUNNING',{setInfo.ActualPS},'{setInfo.LabelBox}')  ON CONFLICT (i_label_serial,i_ws_code,i_factory) DO UPDATE SET i_dte_log_i = EXCLUDED.i_dte_log_i,i_dte_log_u = EXCLUDED.i_dte_log_u,i_usr_log_i = EXCLUDED.i_usr_log_i,i_usr_log_u = EXCLUDED.i_usr_log_u,i_state = EXCLUDED.i_state,i_state_serial = EXCLUDED.i_state_serial,i_reflect = EXCLUDED.i_reflect,i_serial_qty = EXCLUDED.i_serial_qty");

        internal bool SaveSerialSort(SetInfo setInfo) => ExcuteQueryPos($"INSERT INTO t_production_scan (i_factory,i_ws_code,i_label_serial,i_po,i_model,i_line_code,i_dte_created,i_process_code,input_yn,final_yn,i_dte_log_i,i_dte_log_u,i_usr_log_i,i_usr_log_u,i_version,i_state_serial,i_reflect,i_box_no,i_state) VALUES ('{setInfo.Factory}','{setInfo.WSCode}','{setInfo.Serial}','{setInfo.PO}','{setInfo.Model}','{setInfo.Line}',NOW(),'{setInfo.ProcessCode}','{setInfo.Input}','{setInfo.Final}',NOW(),NOW(),'{UserSettings.Email}','{UserSettings.Email}','{setInfo.Verison}','RUNNING','{setInfo.Reflect}','{setInfo.LabelBox}','RUNNING')  ON CONFLICT (i_label_serial,i_ws_code,i_factory) DO UPDATE SET i_dte_log_i = EXCLUDED.i_dte_log_i,i_dte_log_u = EXCLUDED.i_dte_log_u,i_usr_log_i = EXCLUDED.i_usr_log_i,i_usr_log_u = EXCLUDED.i_usr_log_u,i_state = EXCLUDED.i_state,i_state_serial = EXCLUDED.i_state_serial,i_reflect = EXCLUDED.i_reflect,i_box_no = EXCLUDED.i_box_no");

        internal bool UpdateBoxScan(SetInfo setInfo) => ExcuteQueryPos($"UPDATE t_production_scan set i_box_no='{setInfo.LabelBox}',i_state='RUNNING',i_remark='{setInfo.Remark}' where i_po ='{setInfo.PO}' and i_factory='{StaticSetting.FactoryCode}' and (i_box_no is null or i_state is null)");

        internal bool UpdateBoxScanSort(string ws,string serial) => ExcuteQueryPos($"UPDATE t_production_scan set i_box_no='',i_state='DELETED',i_state_serial='DELETED' where i_label_serial in {serial} and i_factory='{StaticSetting.FactoryCode}' and i_ws_code='{ws}'");
        internal bool UpdateRePrint(SetInfo setInfo) => ExcuteQueryPos($"UPDATE t_print_box_label set re_print_reason='{setInfo.Reason}',last_print_time=NOW() where box_no = '{setInfo.LabelBox}' and i_factory_code='{StaticSetting.FactoryCode}'");
        internal bool UpdateProdPlan(SetInfo setInfo, bool mode, string processCode, string processCodeCom)
        {
            if (mode) // fisrt
            {
                return ExcuteQueryPos($"UPDATE t_md_plan set i_actual_qty={setInfo.Actual},i_bal_qty={setInfo.Plan - setInfo.Actual}, i_actual_start_time=NOW(),i_prod_status='{processCode}' where i_factory_code='{setInfo.Factory}' and i_prod_order_no='{setInfo.PO}' and i_factory_code='{StaticSetting.FactoryCode}'");
            }
            else // second
            {
                if (setInfo.Actual == setInfo.Plan)
                    return ExcuteQueryPos($"UPDATE t_md_plan set i_actual_qty={setInfo.Actual},i_bal_qty={setInfo.Plan - setInfo.Actual},i_actual_end_time=NOW(),i_prod_status='{processCodeCom}' where i_prod_order_no='{setInfo.PO}' and i_factory_code='{StaticSetting.FactoryCode}'");
                else return ExcuteQueryPos($"UPDATE t_md_plan set i_actual_qty={setInfo.Actual},i_bal_qty={setInfo.Plan- setInfo.Actual},i_prod_status='{processCode}' where i_prod_order_no='{setInfo.PO}' and i_factory_code='{StaticSetting.FactoryCode}'"); ;
            }
        }
        internal bool UpdatePlan(SetInfo setInfo, string processCode) => ExcuteQueryPos($"UPDATE t_md_plan set i_actual_qty={setInfo.Actual},i_prod_status='{processCode}' where i_prod_order_no='{setInfo.PO}'");
        internal bool UpdateProdPlanBox(string po, int actual, int bal) => ExcuteQueryPos($"UPDATE t_md_plan set i_actual_qty={actual} where i_prod_order_no='{po}' ");
        internal bool UpdateProdFinal(SetInfo setInfo) => ExcuteQueryPos($"UPDATE t_md_plan set i_actual_qty={setInfo.Actual},i_actual_end_time=NOW() where i_factory_code='{setInfo.Factory}' and i_prod_order_no='{setInfo.PO}' and i_factory_code='{StaticSetting.FactoryCode}'");
        internal bool UpdateProd(SetInfo setInfo) => ExcuteQueryPos($"UPDATE t_md_plan set i_actual_qty={setInfo.Actual} where i_factory_code='{setInfo.Factory}' and i_prod_order_no='{setInfo.PO}'");
        internal bool SaveRemark(SetInfo setInfo) => ExcuteQueryPos($"UPDATE t_production_scan set i_remark='{setInfo.Remark}' where i_box_no='{setInfo.LabelBox}' and i_factory='{StaticSetting.FactoryCode}'");
        internal bool SaveBox(SetInfo setInfo, int id)
        {
            if (setInfo.ItemModes == ItemMode.PACKING_SIZE) return ExcuteQueryPos($"INSERT INTO t_print_box_label (print_label_box_id,i_factory_code,i_dte_log_i,i_dte_log_u,i_usr_log_i,i_usr_log_u,i_version,i_state,box_no,qty,box_label_factory_code,last_print_time,label_type,print_type,print_no,box_label_id) VALUES ((select (max(print_label_box_id)+1) as idx from t_print_box_label),'{setInfo.Factory}',NOW(),NOW(),'{UserSettings.Email}','{UserSettings.Email}',0,'RUNNING','{setInfo.LabelBox}','{setInfo.ActualPS}','{setInfo.Factory}',NOW(),'{setInfo.POType}','production',0,{id})");
            else return ExcuteQueryPos($"INSERT INTO t_print_box_label (print_label_box_id,i_factory_code,i_dte_log_i,i_dte_log_u,i_usr_log_i,i_usr_log_u,i_version,i_state,box_no,qty,box_label_factory_code,last_print_time,label_type,print_type,print_no,box_label_id) VALUES ((select (max(print_label_box_id)+1) as idx from t_print_box_label),'{setInfo.Factory}',NOW(),NOW(),'{UserSettings.Email}','{UserSettings.Email}',0,'RUNNING','{setInfo.LabelBox}','{setInfo.Actual}','{setInfo.Factory}',NOW(),'{setInfo.POType}','production',0,{id})");
        }
        internal bool UpdateBoxLabel(int id, string gen, SetInfo setInfo) => ExcuteQueryPos($"INSERT INTO t_box_label (box_label_id,i_factory_code,i_dte_log_i,i_dte_log_u,i_usr_log_i,i_usr_log_u,i_version,i_state,generate_id,generate_qty,generation_time,lot_no,package_qty,print_status,source,purchase_id,purchase_factory_code) VALUES({id},'{setInfo.Factory}',NOW(),NOW(),'{UserSettings.Email}','{UserSettings.Email}',{setInfo.Verison},'{setInfo.State}','{gen}',{setInfo.ActualPS},NOW(),'{setInfo.LotNo}',1,'Generated','OI',{setInfo.ProdGRId},'{setInfo.Factory}')");
        internal DataTable GetBoxLabel() => QueryDataPos($"select max(box_label_id) as id,max(generate_id) as gen from t_box_label where i_factory_code='{StaticSetting.FactoryCode}'");
        internal DataTable GetPO(string box) => QueryDataPos($"select * from t_production_scan where i_box_no='{box}' and i_factory='{StaticSetting.FactoryCode}'");
        internal bool DeleteSerial(string serial, string ws) => ExcuteQueryPos($"UPDATE t_production_scan set i_state_serial='DELETED' where i_label_serial='{serial}' and i_ws_code='{ws}' and i_factory='{StaticSetting.FactoryCode}'");
        internal bool CancelBox(string box) => ExcuteQueryPos($"UPDATE t_print_box_label set i_state='DELETED' where box_no='{box}' and i_factory_code='{StaticSetting.FactoryCode}'");
        internal bool CancelSerialBox(string box, string ws, string factory) => ExcuteQueryPos($"UPDATE t_production_scan set i_state='DELETED',i_state_serial='DELETED' where i_box_no='{box}' and i_ws_code='{ws}' and i_factory='{factory}' and i_factory='{StaticSetting.FactoryCode}'");
        internal DataTable GetPlanDate(string epass, string ws)
        {
            string query = $"select b.i_plan_date as plan_date  from t_production_scan a left join t_md_plan b on a.i_factory=b.i_factory_code and a.i_po=b.i_prod_order_no where ";
            if (epass.Length == 26) query += $"a.i_label_serial";
            else query += $"a.i_box_no";
            query += $"='{epass}' and a.i_ws_code='{ws}' and a.i_factory='{StaticSetting.FactoryCode}'";
            return QueryDataPos(query);
        }
        internal DataTable CheckSerialBox(string serial,string ws) => QueryDataPos($"select i_label_serial ,i_box_no  from t_production_scan where i_label_serial ='{serial}' and i_state='RUNNING' and i_factory='{StaticSetting.FactoryCode}' and i_ws_code='{ws}'");
        internal DataTable GetBoxStatus(string box) => QueryDataPos($"select distinct(i_state) from t_print_box_label where box_no ='{box}' and i_factory_code='{StaticSetting.FactoryCode}'");
        internal DataTable GetPlanQty(string po) => QueryDataPos($"select i_plan_qty from t_md_plan where i_prod_order_no ='{po}' and i_factory_code='{StaticSetting.FactoryCode}'");
        internal DataTable GetFactory() => QueryDataPos($"select *  from t_md_factory where i_state='RUNNING'");
        internal DataTable CheckVersion(string ass) => QueryDataPos($"select *  from t_manage_app_version where i_app_name ='{ass}' and i_yn='Y'");
        internal DataTable GetTimeSerial(string serial) => QueryDataPos($"select i_label_serial,i_dte_created from t_production_scan where i_label_serial ='{serial}' and i_factory='{StaticSetting.FactoryCode}'");
        internal DataTable GetTimeBox(string box) => QueryDataPos($"select box_no,i_dte_log_i from t_print_box_label where box_no ='{box}' and i_factory_code='{StaticSetting.FactoryCode}'");
        internal DataTable GetLabelBox(SetInfo setInfo) => QueryDataPos($"select max(box_label) as box_no from (select max(substring(box_no,14,11)) as box_label from t_stock_movement where lot_no='{setInfo.LotNo}' and material_id={setInfo.ModelID} and i_factory_code='{StaticSetting.FactoryCode}' union all select max(substring(box_no,14,11)) as box_label from t_print_box_label where substring(box_no,14,7)='{setInfo.LotNo}' and  substring(box_no,3,7)='{setInfo.Model}' and i_factory_code='{StaticSetting.FactoryCode}') as t");
        internal DataTable GetLabelBoxSerial(string box) => QueryDataPos($"select * from t_production_scan where i_box_no in ({box}) and i_factory='{StaticSetting.FactoryCode}' and i_state='RUNNING' order by i_box_no asc");
        internal DataTable GetLabelBoxSorting(string box,string ws) => QueryDataPos($"select a.i_label_serial, b.i_code,b.i_name as model_name,a.i_po,d.lot_no,a.i_line_code,e.i_name,a.i_dte_created,f.i_name as factory_name,c.i_po_type,a.i_model as model,c.i_version from t_production_scan a left join t_md_material_master b on b.i_material_id = a.i_model and b.i_factory_code=a.i_factory left join t_md_plan c on c.i_factory_code=a.i_factory and c.i_prod_order_no=a.i_po left join t_production_label d on d.plan_id= c.i_id and d.i_factory_code=a.i_factory left join t_md_line e on e.i_factory_code=a.i_factory and e.i_code=a.i_line_code left join t_md_factory f on f.i_factory_code=a.i_factory where a.i_box_no = '{box}' and a.i_ws_code='{ws}' and f.i_state='RUNNING' and a.i_factory='{StaticSetting.FactoryCode}' and a.i_state='RUNNING' order by i_label_serial asc");
        internal DataTable CheckBoxStock(string box) => QueryDataPos($"select * from t_stock_movement where box_no = '{box}' and i_factory_code='{StaticSetting.FactoryCode}' and i_state='RUNNING'");
        internal DataTable GetLabelBoxSet(SetInfo setInfo) => QueryDataPos($"select max(box_label) as box_no from (select max(substring(box_no,14,11)) as box_label from t_stock_movement where material_id={setInfo.ModelID} and i_factory_code='{StaticSetting.FactoryCode}' union all select max(substring(box_no,14,11)) as box_label from t_print_box_label where substring(box_no,3,7)='{setInfo.Model}' and i_factory_code='{StaticSetting.FactoryCode}') as t");
        internal bool SaveConfig(ProgramSettings settings) => ExcuteQueryPos($"INSERT INTO t_rm_prod_result_config (i_screen_id,i_screen_name,i_factory,i_ipaddss,i_line_name,i_line_code,i_process_name,i_process_code,i_ws_name,i_ws_code,i_short_screen,i_config,i_created,i_factory_code,i_mac) VALUES ('{settings.ScreenID}','{settings.ScreenName}','{settings.LineInfos.Factory}','{settings.IpAddress}','{settings.LineInfos.LineName}','{settings.LineInfos.LineCode}','{settings.LineInfos.ProcessName}','{settings.LineInfos.ProcessCode}','{settings.LineInfos.WSName}','{settings.LineInfos.WSCode}','{settings.ShortScreen}','{JsonConvert.SerializeObject(settings)}',NOW(),'{settings.LineInfos.FactoryCode}','{StaticSetting.MacAddress}') ON CONFLICT (i_mac) DO UPDATE SET i_screen_name = EXCLUDED.i_screen_name,i_factory = EXCLUDED.i_factory,i_ipaddss = EXCLUDED.i_ipaddss,i_line_name = EXCLUDED.i_line_name,i_line_code = EXCLUDED.i_line_code,i_process_name = EXCLUDED.i_ws_code,i_process_code = EXCLUDED.i_process_code,i_ws_name = EXCLUDED.i_ws_name,i_ws_code = EXCLUDED.i_ws_code,i_short_screen = EXCLUDED.i_short_screen,i_config = EXCLUDED.i_config,i_factory_code = EXCLUDED.i_factory_code,i_screen_id = EXCLUDED.i_screen_id");

    }
}
