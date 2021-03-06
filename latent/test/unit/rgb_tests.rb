require File.join '.', 'test', 'helper'

describe 'it can import rgb formatted files' do
  it 'should expect an object that can be read' do
    importer = Latent::UseCases.rgb

    assert_raises (NoMethodError) do
      importer.import 'foo'
    end
  end

  it 'should return no order for an empty order' do
    importer = Latent::UseCases.rgb
    mock = MockFile.new ''

    order = importer.import mock

    assert_equal nil, order
  end

  it 'should be able to interpret a volume, regardless of colour' do
    importer = Latent::UseCases.rgb
    mock = MockFile.new '10'

    order = importer.import mock

    assert_equal 10, order.volume
  end

  it 'should not have a valid colour tho' do
    importer = Latent::UseCases.rgb
    mock = MockFile.new '10'

    order = importer.import mock
    assert_equal nil, order.colour
  end

  it 'should not record an order that does\'t have a valid colour' do
    importer = Latent::UseCases.rgb
    mock = MockFile.new '10'

    order = importer.import mock
    # TODO
    # how can I assert that no order was recorded tho?
  end

  it 'should be able to record a volume alongside a valid rgb colour spec' do
    importer = Latent::UseCases.rgb
    mock = MockFile.new '10,255,0,0'

    order = importer.import mock

    assert_equal 10, order.volume
    assert order.colour.is_red
  end

  it 'should raise an error when you give it a channel above 255' do
    importer = Latent::UseCases.rgb
    mock = MockFile.new '10,256,0,0'

    assert_raises RuntimeError do
      importer.import mock
    end
  end

  it 'should raise an error when you give it a channel below 0' do
    importer = Latent::UseCases.rgb
    mock = MockFile.new '10,-1,0,0'

    assert_raises RuntimeError do
      importer.import mock
    end
  end

  it 'should be able to discern blue from red' do
    importer = Latent::UseCases.rgb
    mock = MockFile.new '10,0,0,255'

    order = importer.import mock

    assert order.colour.is_blue
  end

  it 'should likewise be able to discern green from blue' do
    importer = Latent::UseCases.rgb
    mock = MockFile.new '10,0,255,0'

    order = importer.import mock

    assert order.colour.is_green
  end

  it 'should not need to take whitespace into account' do
    importer = Latent::UseCases.rgb
    mock = MockFile.new '10, 0, 255, 0'

    order = importer.import mock

    assert_equal 10, order.volume
    assert order.colour.is_green
  end

  it 'should still have a colour if the volume is zero' do
    importer = Latent::UseCases.rgb
    mock = MockFile.new '0,255,0,0'

    order = importer.import mock

    assert_equal 0, order.volume
    assert order.colour.is_red
  end

  it 'should not record an order (if the volume is zero)' do
    importer = Latent::UseCases.rgb
    mock = MockFile.new '0,255,0,0'

    order = importer.import mock

    assert_equal 0, order.volume
    # TODO
    # how can we assert no order was recorded tho?
  end

  it 'should be able to take multiple orders when spread across multiple lines' do
    importer = Latent::UseCases.rgb
    mock = MockFile.new '5,255,0,0\n5,255,0,0'

    order = importer.import mock

    assert_equal 2, order.length
    assert (order.at 0).colour.is_red
    assert (order.at 1).colour.is_red
    assert 5, (order.at 0).volume
  end

  it 'should record the overall order as the sum of the consituent colours' do
    importer = Latent::UseCases.rgb
    mock = MockFile.new '5,255,0,0\n5,255,0,0'

    order = importer.import mock

    # TODO, how to make assertions about the order recorder
  end

  it 'should record the overall orders for each separate colour imported' do
    importer = Latent::UseCases.rgb
    mock = MockFile.new '5,255,0,0\n5,0,255,0'

    order = importer.import mock

    # TODO, how to make assertions about the order recorder
  end
end
