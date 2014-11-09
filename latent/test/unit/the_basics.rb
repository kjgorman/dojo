require File.join '.', 'test', 'helper'

describe 'Whats\'s this all about' do
  it 'can create an rgb importer' do
    assert Latent::UseCases.rgb.respond_to? :import
  end

  it 'can create a cmyk importer' do
    assert Latent::UseCases.cmyk.respond_to? :import
  end
end
