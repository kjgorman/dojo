require 'minitest/autorun'
require 'minitest/pride'

require File.join '.', 'lib', 'latent'
Dir.glob(File.join('.', 'test', 'support', '**', '*.rb')).each {|f| require f}

include Latent
