require File.join '.', 'test', 'helper'

describe 'it can import cmyk formatted files' do
  it 'should expect an object that can be read' do
    importer = Latent::UseCases.cmyk

    assert_raises (NoMethodError) do
      importer.import 'foo'
    end
  end

  it 'should return no order for an empty order' do
    importer = Latent::UseCases.cmyk
    mock = MockFile.new ''

    order = importer.import mock

    assert_equal nil, order
  end

  it 'should return no order for an empty order' do
    importer = Latent::UseCases.cmyk
    mock = MockFile.new ''

    order = importer.import mock

    assert_equal nil, order
  end

  it 'should be able to read a volume' do
    importer = Latent::UseCases.cmyk
    mock = MockFile.new '10'

    order = importer.import mock

    assert_equal 10, order.volume
  end

  it 'should not have a valid colour tho' do
    importer = Latent::UseCases.cmyk
    mock = MockFile.new '10'

    order = importer.import mock
    assert_equal nil, order.colour
  end

  it 'should not record an order that does\'t have a valid colour' do
    importer = Latent::UseCases.cmyk
    mock = MockFile.new '10'

    order = importer.import mock
    # TODO
    # how can I assert that no order was recorded tho?
  end

  it 'should be able to record a volume alongside a valid cmyk colour spec' do
    importer = Latent::UseCases.cmyk
    mock = MockFile.new '10,0,100,100,0'

    order = importer.import mock

    assert_equal 10, order.volume
    assert order.colour.is_red
  end

  it 'should be able to discern blue from red' do
    importer = Latent::UseCases.cmyk
    mock = MockFile.new '10,100,100,0,0'

    order = importer.import mock

    assert_equal 10, order.volume
    assert order.colour.is_blue
  end

  it 'should be able to convert multiple lines' do
    importer = Latent::UseCases.cmyk
    mock = MockFile.new '10,0,100,100,0\n10,100,100,0,0'

    order = importer.import mock

    assert_equal 2, order.length
    assert_equal 10, (order.at 0).volume
    assert (order.at 0).colour.is_red
    assert (order.at 1).colour.is_blue
  end

  it 'records the sum of the volume of the same colour' do
    importer = Latent::UseCases.cmyk
    mock = MockFile.new '10,100,100,0,0\n10,100,100,0,0'

    order = importer.import mock

    # TODO
    # how can we assert we ordered 20L of blue?
  end

  it 'records the parts of volume per colour involved' do
    importer = Latent::UseCases.cmyk
    mock = MockFile.new '10,0,100,100,0\n100,100,0,0'

    order = importer.import mock

    # TODO
    # how can we assert we have 10L red and 10L blue?
  end
end
