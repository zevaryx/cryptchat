# -*- coding: utf-8 -*-
# Generated by the protocol buffer compiler.  DO NOT EDIT!
# source: message.proto

import sys
_b=sys.version_info[0]<3 and (lambda x:x) or (lambda x:x.encode('latin1'))
from google.protobuf import descriptor as _descriptor
from google.protobuf import message as _message
from google.protobuf import reflection as _reflection
from google.protobuf import symbol_database as _symbol_database
# @@protoc_insertion_point(imports)

_sym_db = _symbol_database.Default()




DESCRIPTOR = _descriptor.FileDescriptor(
  name='message.proto',
  package='cryptchatprotos.responses.message',
  syntax='proto3',
  serialized_options=_b('\252\002!CryptChatProtos.Responses.Message'),
  serialized_pb=_b('\n\rmessage.proto\x12!cryptchatprotos.responses.message\"\xb1\x01\n\x0fMessageResponse\x12\x0b\n\x03_id\x18\x01 \x01(\t\x12\x0c\n\x04\x63hat\x18\x02 \x01(\t\x12\x0f\n\x07message\x18\x03 \x01(\t\x12\x0b\n\x03key\x18\x04 \x01(\t\x12\x11\n\tsignature\x18\x05 \x01(\t\x12\x11\n\ttimestamp\x18\x06 \x01(\x01\x12\x0e\n\x06sender\x18\x07 \x01(\t\x12\x0e\n\x06\x65\x64ited\x18\x08 \x01(\x08\x12\x11\n\tedited_at\x18\t \x01(\x01\x12\x0c\n\x04\x66ile\x18\n \x01(\t\"[\n\x13MessageListResponse\x12\x44\n\x08messages\x18\x01 \x03(\x0b\x32\x32.cryptchatprotos.responses.message.MessageResponse\"C\n\x13SendMessageResponse\x12\x0b\n\x03_id\x18\x01 \x01(\t\x12\x0c\n\x04\x63hat\x18\x02 \x01(\t\x12\x11\n\ttimestamp\x18\x03 \x01(\x01\"E\n\x13\x45\x64itMessageResponse\x12\x0e\n\x06\x65\x64ited\x18\x01 \x01(\x08\x12\x11\n\tedited_at\x18\x02 \x01(\x01\x12\x0b\n\x03_id\x18\x03 \x01(\t\"(\n\x15\x44\x65leteMessageResponse\x12\x0f\n\x07\x64\x65leted\x18\x01 \x01(\x08\x42$\xaa\x02!CryptChatProtos.Responses.Messageb\x06proto3')
)




_MESSAGERESPONSE = _descriptor.Descriptor(
  name='MessageResponse',
  full_name='cryptchatprotos.responses.message.MessageResponse',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='_id', full_name='cryptchatprotos.responses.message.MessageResponse._id', index=0,
      number=1, type=9, cpp_type=9, label=1,
      has_default_value=False, default_value=_b("").decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='chat', full_name='cryptchatprotos.responses.message.MessageResponse.chat', index=1,
      number=2, type=9, cpp_type=9, label=1,
      has_default_value=False, default_value=_b("").decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='message', full_name='cryptchatprotos.responses.message.MessageResponse.message', index=2,
      number=3, type=9, cpp_type=9, label=1,
      has_default_value=False, default_value=_b("").decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='key', full_name='cryptchatprotos.responses.message.MessageResponse.key', index=3,
      number=4, type=9, cpp_type=9, label=1,
      has_default_value=False, default_value=_b("").decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='signature', full_name='cryptchatprotos.responses.message.MessageResponse.signature', index=4,
      number=5, type=9, cpp_type=9, label=1,
      has_default_value=False, default_value=_b("").decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='timestamp', full_name='cryptchatprotos.responses.message.MessageResponse.timestamp', index=5,
      number=6, type=1, cpp_type=5, label=1,
      has_default_value=False, default_value=float(0),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='sender', full_name='cryptchatprotos.responses.message.MessageResponse.sender', index=6,
      number=7, type=9, cpp_type=9, label=1,
      has_default_value=False, default_value=_b("").decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='edited', full_name='cryptchatprotos.responses.message.MessageResponse.edited', index=7,
      number=8, type=8, cpp_type=7, label=1,
      has_default_value=False, default_value=False,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='edited_at', full_name='cryptchatprotos.responses.message.MessageResponse.edited_at', index=8,
      number=9, type=1, cpp_type=5, label=1,
      has_default_value=False, default_value=float(0),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='file', full_name='cryptchatprotos.responses.message.MessageResponse.file', index=9,
      number=10, type=9, cpp_type=9, label=1,
      has_default_value=False, default_value=_b("").decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR),
  ],
  extensions=[
  ],
  nested_types=[],
  enum_types=[
  ],
  serialized_options=None,
  is_extendable=False,
  syntax='proto3',
  extension_ranges=[],
  oneofs=[
  ],
  serialized_start=53,
  serialized_end=230,
)


_MESSAGELISTRESPONSE = _descriptor.Descriptor(
  name='MessageListResponse',
  full_name='cryptchatprotos.responses.message.MessageListResponse',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='messages', full_name='cryptchatprotos.responses.message.MessageListResponse.messages', index=0,
      number=1, type=11, cpp_type=10, label=3,
      has_default_value=False, default_value=[],
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR),
  ],
  extensions=[
  ],
  nested_types=[],
  enum_types=[
  ],
  serialized_options=None,
  is_extendable=False,
  syntax='proto3',
  extension_ranges=[],
  oneofs=[
  ],
  serialized_start=232,
  serialized_end=323,
)


_SENDMESSAGERESPONSE = _descriptor.Descriptor(
  name='SendMessageResponse',
  full_name='cryptchatprotos.responses.message.SendMessageResponse',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='_id', full_name='cryptchatprotos.responses.message.SendMessageResponse._id', index=0,
      number=1, type=9, cpp_type=9, label=1,
      has_default_value=False, default_value=_b("").decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='chat', full_name='cryptchatprotos.responses.message.SendMessageResponse.chat', index=1,
      number=2, type=9, cpp_type=9, label=1,
      has_default_value=False, default_value=_b("").decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='timestamp', full_name='cryptchatprotos.responses.message.SendMessageResponse.timestamp', index=2,
      number=3, type=1, cpp_type=5, label=1,
      has_default_value=False, default_value=float(0),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR),
  ],
  extensions=[
  ],
  nested_types=[],
  enum_types=[
  ],
  serialized_options=None,
  is_extendable=False,
  syntax='proto3',
  extension_ranges=[],
  oneofs=[
  ],
  serialized_start=325,
  serialized_end=392,
)


_EDITMESSAGERESPONSE = _descriptor.Descriptor(
  name='EditMessageResponse',
  full_name='cryptchatprotos.responses.message.EditMessageResponse',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='edited', full_name='cryptchatprotos.responses.message.EditMessageResponse.edited', index=0,
      number=1, type=8, cpp_type=7, label=1,
      has_default_value=False, default_value=False,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='edited_at', full_name='cryptchatprotos.responses.message.EditMessageResponse.edited_at', index=1,
      number=2, type=1, cpp_type=5, label=1,
      has_default_value=False, default_value=float(0),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR),
    _descriptor.FieldDescriptor(
      name='_id', full_name='cryptchatprotos.responses.message.EditMessageResponse._id', index=2,
      number=3, type=9, cpp_type=9, label=1,
      has_default_value=False, default_value=_b("").decode('utf-8'),
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR),
  ],
  extensions=[
  ],
  nested_types=[],
  enum_types=[
  ],
  serialized_options=None,
  is_extendable=False,
  syntax='proto3',
  extension_ranges=[],
  oneofs=[
  ],
  serialized_start=394,
  serialized_end=463,
)


_DELETEMESSAGERESPONSE = _descriptor.Descriptor(
  name='DeleteMessageResponse',
  full_name='cryptchatprotos.responses.message.DeleteMessageResponse',
  filename=None,
  file=DESCRIPTOR,
  containing_type=None,
  fields=[
    _descriptor.FieldDescriptor(
      name='deleted', full_name='cryptchatprotos.responses.message.DeleteMessageResponse.deleted', index=0,
      number=1, type=8, cpp_type=7, label=1,
      has_default_value=False, default_value=False,
      message_type=None, enum_type=None, containing_type=None,
      is_extension=False, extension_scope=None,
      serialized_options=None, file=DESCRIPTOR),
  ],
  extensions=[
  ],
  nested_types=[],
  enum_types=[
  ],
  serialized_options=None,
  is_extendable=False,
  syntax='proto3',
  extension_ranges=[],
  oneofs=[
  ],
  serialized_start=465,
  serialized_end=505,
)

_MESSAGELISTRESPONSE.fields_by_name['messages'].message_type = _MESSAGERESPONSE
DESCRIPTOR.message_types_by_name['MessageResponse'] = _MESSAGERESPONSE
DESCRIPTOR.message_types_by_name['MessageListResponse'] = _MESSAGELISTRESPONSE
DESCRIPTOR.message_types_by_name['SendMessageResponse'] = _SENDMESSAGERESPONSE
DESCRIPTOR.message_types_by_name['EditMessageResponse'] = _EDITMESSAGERESPONSE
DESCRIPTOR.message_types_by_name['DeleteMessageResponse'] = _DELETEMESSAGERESPONSE
_sym_db.RegisterFileDescriptor(DESCRIPTOR)

MessageResponse = _reflection.GeneratedProtocolMessageType('MessageResponse', (_message.Message,), {
  'DESCRIPTOR' : _MESSAGERESPONSE,
  '__module__' : 'message_pb2'
  # @@protoc_insertion_point(class_scope:cryptchatprotos.responses.message.MessageResponse)
  })
_sym_db.RegisterMessage(MessageResponse)

MessageListResponse = _reflection.GeneratedProtocolMessageType('MessageListResponse', (_message.Message,), {
  'DESCRIPTOR' : _MESSAGELISTRESPONSE,
  '__module__' : 'message_pb2'
  # @@protoc_insertion_point(class_scope:cryptchatprotos.responses.message.MessageListResponse)
  })
_sym_db.RegisterMessage(MessageListResponse)

SendMessageResponse = _reflection.GeneratedProtocolMessageType('SendMessageResponse', (_message.Message,), {
  'DESCRIPTOR' : _SENDMESSAGERESPONSE,
  '__module__' : 'message_pb2'
  # @@protoc_insertion_point(class_scope:cryptchatprotos.responses.message.SendMessageResponse)
  })
_sym_db.RegisterMessage(SendMessageResponse)

EditMessageResponse = _reflection.GeneratedProtocolMessageType('EditMessageResponse', (_message.Message,), {
  'DESCRIPTOR' : _EDITMESSAGERESPONSE,
  '__module__' : 'message_pb2'
  # @@protoc_insertion_point(class_scope:cryptchatprotos.responses.message.EditMessageResponse)
  })
_sym_db.RegisterMessage(EditMessageResponse)

DeleteMessageResponse = _reflection.GeneratedProtocolMessageType('DeleteMessageResponse', (_message.Message,), {
  'DESCRIPTOR' : _DELETEMESSAGERESPONSE,
  '__module__' : 'message_pb2'
  # @@protoc_insertion_point(class_scope:cryptchatprotos.responses.message.DeleteMessageResponse)
  })
_sym_db.RegisterMessage(DeleteMessageResponse)


DESCRIPTOR._options = None
# @@protoc_insertion_point(module_scope)