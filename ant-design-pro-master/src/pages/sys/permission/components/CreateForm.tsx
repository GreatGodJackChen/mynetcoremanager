import { Form, Input, Modal, Select } from 'antd';

import { FormComponentProps } from 'antd/es/form';
import React from 'react';
import TreeBind from './Tree/index';
const FormItem = Form.Item;
interface CreateFormProps extends FormComponentProps {
  modalVisible: boolean;
  handleAdd: (fieldsValue: {
    newname: string,
    newactionCode: string,
    newmenuId:string,
  }) => void;
  handleModalVisible: () => void;
}

const CreateForm: React.SFC<CreateFormProps> = props => {
  const { modalVisible, form, handleAdd, handleModalVisible } = props;
  const okHandle = () => {
    form.validateFields((err, fieldsValue) => {
      if (err) return;
      form.resetFields();
      handleAdd(fieldsValue);
    });
  };
  return (
    <Modal
      destroyOnClose
      title="新增"
      visible={modalVisible}
      onOk={okHandle}
      onCancel={() => handleModalVisible()}
    >
      <FormItem labelCol={{ span: 5 }} wrapperCol={{ span: 15 }} label="名称">
        {form.getFieldDecorator('newname', {
          rules: [{ required: true, message: '请输入至少2个字符的规则描述！', min: 2 }],
        })(<Input placeholder="请输入" />)}
      </FormItem>
      <FormItem labelCol={{ span: 5 }} wrapperCol={{ span: 15 }} label="操作码">
        {form.getFieldDecorator('newactionCode', {
          rules: [{ required: true, message: '请输入至少2个字符的规则描述！', min: 2 }],
        })(<Input placeholder="请输入" />)}
      </FormItem>
      <FormItem  labelCol={{ span: 5 }} wrapperCol={{ span: 15 }} label="关联菜单">
        {form.getFieldDecorator('newmenuId', {
          rules: [{ required: true,}],
        })(<TreeBind />)}
      </FormItem>
    </Modal>
  );
};

export default CreateForm;
